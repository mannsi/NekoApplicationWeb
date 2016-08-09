using System.Threading.Tasks;
using CreditScore;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface ICreditScoreService
    {
        Task<CreditScoreEntry> FetchCreditScore(string ssn);
    }
}