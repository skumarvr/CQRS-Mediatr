using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Events;
using TechChallenge.Domain.ServiceHandlers;

namespace TechChallenge.Domain.Leads.EventHandlers
{
    public class EmailNotificationHandler : INotificationHandler<SendEmailEvent>
    {
        readonly private IEmailServiceHandler _emailService;

        public EmailNotificationHandler(IEmailServiceHandler emailService)
        {
            _emailService = emailService;
        }
        public Task Handle(SendEmailEvent notification, CancellationToken cancellationToken)
        {
            //send message in email
            _emailService.SendEmail(notification.ToSender, notification.Message);
            return Task.FromResult(0);
        }
    }
}
