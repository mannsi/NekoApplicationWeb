using System.Collections.Generic;
using NekoApplicationWeb.Models;


namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface IThjodskraService
    {
        ThjodskraPerson GetUserEntity(string ssn);
        List<ThjodskraFamilyEntry> UserFamilyInfo(string familyNumber);
    }
}