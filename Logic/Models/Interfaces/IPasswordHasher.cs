namespace Logic.Models.Interfaces
{
    public interface IPasswordHasher
    {
        public string Generate(string password);

        public bool VerifyPassword(string password, string hashedPassword);

    }
}
