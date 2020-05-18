using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.Domain.ServiceHandlers
{
    public interface IEmailServiceHandler
    {
        Task SendEmail(string toSender, string message);
    }
}
