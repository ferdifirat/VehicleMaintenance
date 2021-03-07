using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleMaintenance.Business.CustomExtensions
{
    public static class Hashing
    {
        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(4);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        public static bool ValidatePassword(byte[] hashed, string password)
        {
            var hashedPassword = System.Text.Encoding.UTF8.GetString(hashed);
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
