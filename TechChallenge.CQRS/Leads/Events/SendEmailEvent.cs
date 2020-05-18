using MediatR;

namespace TechChallenge.Domain.Leads.Events
{
    public class SendEmailEvent : INotification
    {
        public string ToSender { get; set; }
        public string Message { get; set; }

        public SendEmailEvent(string toSender, string msg)
        {
            this.ToSender = toSender;
            this.Message = msg;
        }
    }
}