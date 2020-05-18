using MediatR;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads.Commands
{
    public class AddNewLeadCommand:IRequest<LeadResponse>
    {
        public int SuburbId { get; set; }
        public int CategoryId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public AddNewLeadCommand(
            int suburbId, 
            int categoryId, 
            string contactName, 
            string contactPhone,
            string contactEmail,
            int price,
            string description)
        {
            this.SuburbId = suburbId;
            this.CategoryId = categoryId;
            this.ContactName = contactName;
            this.ContactPhone = contactPhone;
            this.ContactEmail = ContactEmail;
            this.Price = price;
            this.Description = description;
        }
    }
}
