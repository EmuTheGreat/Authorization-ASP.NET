namespace Logic.Models.Interfaces
{
    public interface IJwtProvider
    {
        public string GenerateToken(User user, string key, string issuer, int expiresHours);
        public bool IsTokenValid(string key, string issuer, string token);
    }
}
