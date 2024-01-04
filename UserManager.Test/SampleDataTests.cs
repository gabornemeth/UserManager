namespace UserManager.Test
{
    public class SampleDataTests
    {
        [Fact]
        public void ParseSampleData()
        {
            var users = SampleData.GetUsers();

            users.Should().HaveCount(10);
            var firstUser = users.First();
            firstUser.Name.Should().Be("Leanne Graham");
            Assert.NotNull(firstUser.Address?.Geolocation);
            firstUser.Address.Geolocation.Latitude.Should().NotBe(0.0f);
            firstUser.Address.Geolocation.Latitude.Should().NotBe(0.0f);
        }
    }
}
