namespace UserManager.Models
{
    public class Address
    {
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string ZipCode { get; set; }
        public string? Suite { get; set; }
        public Location? Geolocation { get; set; }
    }
}
