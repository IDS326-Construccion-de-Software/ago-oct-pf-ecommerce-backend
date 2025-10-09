using Revenge.Infrestructure.Entities;
using System.Security.Principal;

namespace Revenge.Infrestructure.Repositories
{
    public interface IAuthenticationRepository
    {
        //Task<User?> LoginAuth0(
        //    string token,
        //    CancellationToken cancellationToken = default);

        Task<User?> LoginUserAsync(
            string email,
            string plainPassword,
            CancellationToken cancellationToken = default);

        Task<bool> LogoutUserAsync(
            string userId,
            CancellationToken cancellationToken = default);

        Task<bool> AddUserAsync(
            User newUser,
            CancellationToken cancellationToken = default);

        Task<bool> VerifyEmailAsync(
            string userId,
            string verificationCode,
            CancellationToken cancellationToken = default);

        Task<bool> ChangePasswordAsync(
            string userId,
            string currentPassword,
            string newPassword,
            CancellationToken cancellationToken = default);

        Task<bool> SendPasswordResetTokenAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task<bool> ResetPasswordAsync(
            string userId,
            string resetToken,
            string newPassword,
            CancellationToken cancellationToken = default);
    }
}
