using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Extensions.Configuration;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;

namespace NekoApplicationWeb.Services
{
    public class ThjodskraService : IThjodskraService
    {
        readonly string _uppruni;
        readonly string _notandi;
        readonly string _greidandi;
        readonly string _password;

        public ThjodskraService(IConfiguration config)
        {
            _uppruni = config["NEKO_uh.is_uppruni"];
            _notandi = config["NEKO_uh.is_notandi"];
            _greidandi = config["NEKO_uh.is_greidandi"];
            _password = config["NEKO_uh.is_password"];
        }

        public ThjodskraPerson GetUserEntity(string ssn)
        {
            string webServerRequest = $"http://gognxml.uh.is/xml_Service.asmx/XMT0005?p_sUppruni={_uppruni}&p_sKtGreid={_greidandi}&p_sKtNot={_notandi}&p_sPassword={_password}&m_sKennitala={ssn}";
            string webServerResponse;
            try
            {
                webServerResponse = GetWebServerResponse(webServerRequest);
            }
            catch (Exception)
            {
                return null;
            }

            return ThjodskraPerson.FromXml(webServerResponse);
        }

        public List<ThjodskraFamilyEntry> UserFamilyInfo(string familyNumber)
        {
            string webServerRequest = $"http://gognxml.uh.is/xml_Service.asmx/XMT0006?p_sUppruni={_uppruni}&p_sKtGreid={_greidandi}&p_sKtNot={_notandi}&p_sPassword={_password}&m_sKennitala={familyNumber}";
            string webServerResponse;
            try
            {
                webServerResponse = GetWebServerResponse(webServerRequest);
            }
            catch (Exception)
            {
                return null;
            }

            return ThjodskraFamilyEntry.FromXml(webServerResponse);
        }

        private string GetWebServerResponse(string requestUrl)
        {
            WebRequest individualRequest = WebRequest.Create(requestUrl);
            HttpWebResponse response = (HttpWebResponse)individualRequest.GetResponse();
            // Display the status.
            Console.WriteLine(response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            return responseFromServer;
        }
    }
}
