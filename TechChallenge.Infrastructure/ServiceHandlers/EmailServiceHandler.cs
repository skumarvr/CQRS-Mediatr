using System.Threading.Tasks;
using TechChallenge.Domain.ServiceHandlers;

namespace TechChallenge.Infrastructure.ServiceHandlers
{
    public class EmailServiceHandler : IEmailServiceHandler
    {
        public Task SendEmail(string toSender, string message)
        {
            // Send Email
            return null;
        }
    }
}
