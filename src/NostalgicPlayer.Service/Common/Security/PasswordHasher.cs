namespace NostalgicPlayer.Service.Common.Security;

public class PasswordHasher
{
    public static (string Hash, string Salt) Hasher(string password)
    {
        string salt = Guid.NewGuid().ToString();
        string hash = BCrypt.Net.BCrypt.HashPassword(password + salt);
        return (Hash: hash, Salt: salt);
    }
}