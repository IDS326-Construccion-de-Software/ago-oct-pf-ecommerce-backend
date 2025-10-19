using Revenge.Infrestructure.Entities;
using System.Security.Principal;

namespace Revenge.Infrestructure.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<AuthLoginResult?> LoginUserAsync(
            string email,
            string plainPassword,
            CancellationToken cancellationToken = default);

        Task<bool> LogoutUserAsync(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<bool> AddUserAsync(
            User newUser,
            CancellationToken cancellationToken = default);

        Task<bool> VerifyEmailAsync(
            Guid userId,
            string verificationCode,
            CancellationToken cancellationToken = default);

        Task<bool> ChangePasswordAsync(
            Guid userId,
            string currentPassword,
            string newPassword,
            CancellationToken cancellationToken = default);

        Task<bool> SendPasswordResetTokenAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task<bool> ResetPasswordAsync(
            Guid userId,
            string resetToken,
            string newPassword,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );
    }
}
