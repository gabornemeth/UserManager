namespace UserManager.Contracts.Responses
{
    public class CreateUserResponse(string id, string name)
    {
        public string Id => id;

        public string Name => name;
    }
}
