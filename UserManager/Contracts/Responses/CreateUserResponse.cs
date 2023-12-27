namespace UserManager.Contracts.Responses
{
    public class CreateUserResponse(int id, string name)
    {
        public int Id => id;

        public string Name => name;
    }
}
