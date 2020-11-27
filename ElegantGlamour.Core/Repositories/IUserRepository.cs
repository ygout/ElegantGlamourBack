using System.Linq;
using System.Threading.Tasks;
using ElegantGlamour.Core.Models.Entity.Auth;
using Microsoft.AspNetCore.Identity;

namespace ElegantGlamour.Core.Repositories
{
    public interface IUserRepository
    {
        IQueryable<IdentityUser> Get();
        IdentityUser GetByEmail(string email);
        Task<IdentityResult> Create(IdentityUser user, string password);
        Task<IdentityResult> Delete(IdentityUser user);
        Task<IdentityResult> Update(IdentityUser user);
        UserManager<IdentityUser> GetUserManager();
    }
}