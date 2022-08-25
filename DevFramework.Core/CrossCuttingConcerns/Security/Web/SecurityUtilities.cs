using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace DevFramework.Core.CrossCuttingConcerns.Security.Web
{
    public class SecurityUtilities
    {
        // Belirtilen Ticket'ı çözümleyip Identity nesnesine dönüştürüyoruz.
        public Identity ForsmAuthTicketToIdentity(FormsAuthenticationTicket ticket)
        {
            var list = ticket.UserData.Split('|');

            var identity = new Identity
            {
                Id = SetId(list),
                Name = ticket.Name,
                Email = SetEmail(list),
                AuthenticationType = SetAuthType(),
                IsAuthenticated = SetIsAuthenticated(),
                FirstName = SetFirstName(list),
                LastName = SetLastName(list),
                Roles = SetRoles(list)
            };

            return identity;
        }

        private string SetAuthType()
        {
            return "Forms";
        }
        private bool SetIsAuthenticated()
        {
            return true;
        }

        private string[] SetRoles(string[] list)
        {
            return list.FirstOrDefault(r => r.StartsWith("ro="))?.Substring(3).Split(',');
        }

        private string SetLastName(string[] list)
        {
            return list.FirstOrDefault(r => r.StartsWith("la="))?.Substring(3);
        }

        private string SetFirstName(string[] list)
        {
            return list.FirstOrDefault(r => r.StartsWith("fi="))?.Substring(3);
        }

        private string SetEmail(string[] list)
        {
            return list.FirstOrDefault(r => r.StartsWith("em="))?.Substring(3);
        }

        private Guid SetId(string[] list)
        {
            return new Guid(list.FirstOrDefault(r => r.StartsWith("id="))?.Substring(3));
        }
    }
}
