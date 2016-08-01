using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> CreateUser(string ssn, string userName, bool isDeletable);
    }
}