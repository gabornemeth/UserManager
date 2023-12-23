using System.Text.Json.Serialization;

namespace UserManager.Mongo
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Suite { get; set; }
        public Location Location { get; set; }
    }

    public class Location
    {
        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
