using System.Collections.Generic;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface IInterestsService
    {
        List<InterestsInfo> GetInterestsMatrix(string lenderId);
    }
}