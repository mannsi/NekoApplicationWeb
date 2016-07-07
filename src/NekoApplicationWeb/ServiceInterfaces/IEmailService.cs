using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface IEmailService
    {
        void SendEmailAsync(string emailAddress, string subject, string message);
    }
}
