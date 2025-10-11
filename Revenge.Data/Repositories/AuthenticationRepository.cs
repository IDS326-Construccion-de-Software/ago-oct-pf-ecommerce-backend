using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;
using Revenge.Data.Models;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> LoginUserAsync(string email, string plainPassword, CancellationToken ct = default)
        {
            var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email, ct);

            if (user is null) return null;

            return null; // Password incorrecto
        }

        public Task<bool> LogoutUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPasswordAsync(Guid userId, string resetToken, string newPassword, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendPasswordResetTokenAsync(string email, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyEmailAsync(Guid userId, string verificationCode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        // ===== MÉTODOS NUEVOS PARA AUTH0 (Por implementar) =====
        
        // public async Task<bool> SaveUserProfileAsync(User user, CancellationToken cancellationToken)
        // {
        //     try
        //     {
        //         user.Password = null;
        //         await _context.Users.AddAsync(user, cancellationToken);
        //         return await _context.SaveChangesAsync(cancellationToken) > 0;
        //     }
        //     catch
        //     {
        //         return false;
        //     }
        // }

        // public async Task<User?> GetUserProfileByEmailAsync(string email, CancellationToken cancellationToken)
        // {
        //     return await _context.Users
        //         .AsNoTracking()
        //         .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        // }

        // public async Task<User?> GetUserProfileByAuth0IdAsync(string auth0UserId, CancellationToken cancellationToken)
        // {
        //     return await _context.Users
        //         .AsNoTracking()
        //         .FirstOrDefaultAsync(u => u.Auth0UserId == auth0UserId, cancellationToken);
        // }

        // public async Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken)
        // {
        //     return await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
        // }
        
        public async Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId, cancellationToken);
        }
    }
}
