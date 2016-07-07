using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface IThjodskraService
    {
        DataEntity GetUserEntity(string ssn);
    }
}