using System.Threading.Tasks;
using ElegantGlamour.Core.Models.Entity.Auth;
using Microsoft.AspNetCore.Identity;

namespace ElegantGlamour.Core.Services
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUser(User user, string password);
        Task<IdentityResult> Delte(User user);
        Task<IdentityResult> Update(User userTobeUpdate, User user);
        Task<IdentityResult> ValidatePassword(User user, string password);
        Task<IdentityResult> ValidateUSer(User user);
        string HashPassword(User user, string password);
        Task SignOutAsync();
        Task<SignInResult> PasswordSignInAsync(User user, string password, bool lockoutOnFailure, bool isPersistent);
    }
}