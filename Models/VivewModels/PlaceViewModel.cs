namespace MyMvcApp.Models
{
    public class PlaceViewModel
    {
        public string Name { get; set; }     
        public string Address { get; set; }   

         public List<Place> places = new List<Place>();
    }
}
