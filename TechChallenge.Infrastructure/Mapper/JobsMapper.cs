using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechChallenge.Data.EntityModels;
using TechChallenge.Domain.Leads.Commands;
using TechChallenge.Domain.Leads.Models;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Infrastructure.Mapper
{
    public static class JobsMapper
    {
        public static List<InvitedLeadsResponse> ToInvitedLeadsResponse(this List<Jobs> jobs)
        {
            var result = jobs.Select(x => new InvitedLeadsResponse() {
                        Id = x.Id,
                        ContactName = x.ContactName,
                        CreatedDateTime = x.CreatedAt.DateTime,
                        Suburb = x.Suburb.Name,
                        CategoryName = x.Category.Name,
                        Description = x.Description,
                        Price = x.Price.ToString()
                    }).ToList();

            return result;
        }

        public static Jobs ToJobs(AddNewLeadCommand request)
        {
            var result = new Jobs()
            {
                Status = LeadStatusType.New.ToString(),
                ContactName = request.ContactName,
                SuburbId = request.SuburbId,
                CategoryId = request.CategoryId,
                Description = request.Description,
                Price = request.Price,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                CreatedAt = DateTime.Now
            };

            return result;
        }

        public static List<AcceptedLeadsResponse> ToAcceptedLeadsResponse(this List<Jobs> jobs)
        {
            var result = jobs.Select(x => new AcceptedLeadsResponse()
            {
                Id = x.Id,
                ContactName = x.ContactName,
                CreatedDateTime = x.CreatedAt.DateTime,
                Suburb = x.Suburb.Name,
                CategoryName = x.Category.Name,
                Description = x.Description,
                Price = x.Price.ToString(),
                ContactEmail = x.ContactEmail,
                ContactNumber = x.ContactPhone,
                Postcode = x.Suburb.Postcode
            }).ToList();

            return result;
        }

        public static LeadResponse ToLeadResponse(this Jobs jobs)
        {
            var result = new LeadResponse
            {
                Id = jobs.Id,
                ContactName = jobs.ContactName,
                CreatedDateTime = jobs.CreatedAt.DateTime,
                Suburb = jobs.Suburb.Name,
                CategoryName = jobs.Category.Name,
                Description = jobs.Description,
                Price = jobs.Price.ToString(),
                ContactEmail = jobs.ContactEmail,
                ContactNumber = jobs.ContactPhone,
                Postcode = jobs.Suburb.Postcode
            };

            return result;
        }
    }
}
