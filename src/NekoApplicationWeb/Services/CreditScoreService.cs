using System;
using System.ServiceModel;
using System.Threading.Tasks;
using CreditScore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NekoApplicationWeb.Services
{
    public class CreditScoreService : ICreditScoreService
    {
        private readonly IConfiguration _config;

        public CreditScoreService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<CreditScoreEntry> FetchCreditScore(string ssn)
        {
            var username = _config["NekoCiCreditScoreUserName"];
            var pw = _config["NekoCiCreditScorePassword"];
            var nekoKt = _config["NekoCiCreditScoreUserKt"];
            var endpointAddress = _config["NekoCiCreditScoreEndpointAddress"];
            var testSubjectKt = "0110556219";// First demo user in documentation

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);

            var client = new CIPIndividualScoreServiceSoapClient(binding, new EndpointAddress(endpointAddress));
            var result = await client.ScoreAsync(username, pw, nekoKt, testSubjectKt, "");
            return CreditScoreEntry.FromWebServiceObject(result.Body.ScoreResult);
        } 

    }
}
