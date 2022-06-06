namespace LocationToImages.WebApi.Token
{
    public interface IJwtManager
    {
        string GenerateToken(string token, int expireMinutes=30);

        string ValidateToken(string token);
    }
}