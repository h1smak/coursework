namespace BlaBlaApi.Models
{
    public class Location
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static bool IsValid(string name, double lat, double lng)
        {
            return !string.IsNullOrWhiteSpace(name) && lat != 0 && lng != 0;
        }
    }


}
