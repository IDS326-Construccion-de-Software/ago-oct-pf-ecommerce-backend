using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;
using Revenge.Data.Models;

namespace Revenge.Data.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly RevengeDbContext _context;

        public AuthenticationRepository(RevengeDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUserAsync(User newUser, CancellationToken cancellationToken = default)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email, cancellationToken);

                if (existingUser != null)
                {
                    return false;
                }
                _context.Users.Add(newUser);

                var result = await _context.SaveChangesAsync(cancellationToken);
                return result > 0;

            }
            catch
            {
                return false;
            }
        }

        public Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> LoginUserAsync(string userId, string plainPassword, CancellationToken cancellationToken = default)
        {

            var user = await _context.Users.FirstOrDefaultAsync(cancellationToken);

            if (user != null)
            {
                return user;
            }

            throw new Exception("not connected");

        }

        public Task<bool> LogoutUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPasswordAsync(string userId, string resetToken, string newPassword, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendPasswordResetTokenAsync(string email, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyEmailAsync(string userId, string verificationCode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
