using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Revenge.Data.Context;
using Revenge.Data.Models;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Revenge.Data.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly RevengeDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AuthenticationRepository(RevengeDbContext context, IConfiguration configuration, HttpClient httpClient)
        {
            _context = context;
            _configuration = configuration;
            _httpClient = httpClient;
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthLoginResult?> LoginUserAsync(string email, string plainPassword, CancellationToken cancellationToken = default)
        {
            var domain = _configuration["Auth0:Domain"];
            var clientId = _configuration["Auth0:ClientId"];
            var clientSecret = _configuration["Auth0:ClientSecret"];
            var audience = _configuration["Auth0:Audience"]; // opcional si necesitas token para tu API

            var tokenRequest = new
            {
                grant_type = "password",
                username = email,
                password = plainPassword,
                client_id = clientId,
                client_secret = clientSecret,
                audience = audience,
                scope = "openid profile email"
            };

            var resp = await _httpClient.PostAsJsonAsync(
                $"https://{domain}/oauth/token",
                tokenRequest,
                cancellationToken);

            var raw = await resp.Content.ReadAsStringAsync(cancellationToken);

            if (!resp.IsSuccessStatusCode)
            {
                // Auth0 devuelve invalid_grant para credenciales erróneas o email no verificado
                if (raw.Contains("invalid_grant", StringComparison.OrdinalIgnoreCase))
                    return null;

                throw new Exception($"Auth0 error: {raw}");
            }

            using var doc = JsonDocument.Parse(raw);
            var root = doc.RootElement;

            return new AuthLoginResult
            {
                AccessToken = root.GetProperty("access_token").GetString()!,
                IdToken = root.TryGetProperty("id_token", out var idt) ? idt.GetString() : null,
                TokenType = root.GetProperty("token_type").GetString()!,
                ExpiresIn = root.GetProperty("expires_in").GetInt32()
            };
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
