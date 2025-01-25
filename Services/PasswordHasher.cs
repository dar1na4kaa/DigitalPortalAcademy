using DigitalPortalAcademy.Models;
using System.Security.Cryptography;
using System.Text;

namespace DigitalPortalAcademy.Services
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
