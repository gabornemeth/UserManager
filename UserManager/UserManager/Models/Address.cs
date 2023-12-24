namespace UserManager.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Suite { get; set; }
        public Location Location { get; set; }
    }

    public record Location(float Latitude, float Longitude);
}
