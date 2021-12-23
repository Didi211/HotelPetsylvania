namespace BekendDeo.AuthentificationService
{
    public interface ITokenManager
    {
        string GenerateToken(int userid);
    }
}