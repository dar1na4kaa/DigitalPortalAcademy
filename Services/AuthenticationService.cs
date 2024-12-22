﻿using DigitalPortalAcademy.Models;
using Microsoft.AspNetCore.Identity;

namespace DigitalPortalAcademy.Services
{
    public class AuthenticationService
    {
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string enteredPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, enteredPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}