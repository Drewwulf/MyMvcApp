using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Services;

public class HomeworkNotificationService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IWebHostEnvironment _environment;

    public HomeworkNotificationService(
        IServiceScopeFactory scopeFactory,
        IWebHostEnvironment environment)
    {
        _scopeFactory = scopeFactory;
        _environment = environment;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            var now = DateTime.UtcNow;
            var tomorrow = now.AddHours(24);

            var homeworkStudents = await context.Homeworks
    .Where(h =>
        h.SubmitTime > now &&
        h.SubmitTime <= tomorrow)
    .SelectMany(h => h.StudentsToHomework
        .Where(sh => !sh.NotificationSent))
    .ToListAsync(stoppingToken);

            foreach (var homework in homeworkStudents)
            {
                await SendNotification(homework);

                homework.NotificationSent = true;
            }

            await context.SaveChangesAsync(stoppingToken);

            // Перевіряти раз на годину
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task SendNotification(StudentsToHomework homeworkts)
    {
        var emails = new EmailSender();

        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        var homework = context.Homeworks.Find(homeworkts.HomeworkId);
        var student = context.Students.Where(s => s.Id == homeworkts.StudentId).FirstOrDefault();
        var studentAsUser = context.Users.Where(u => u.Id == student.UserId).First();
        await emails.SendEmailAsync(studentAsUser.NormalizedUserName, studentAsUser.Email, "Вам наначили нове домашнє завдання! School",
            $"""
                    <b>Нове домашнє завдання!</b>

                    <b>Назва:</b> {homework.HomeworkName}

                    <b>Опис:</b> {homework.HomeworkDescription}

                    <b>Виконати до:</b> {homework.SubmitTime}

                    Бажаємо успіхів!
                 """, true);
    }
}