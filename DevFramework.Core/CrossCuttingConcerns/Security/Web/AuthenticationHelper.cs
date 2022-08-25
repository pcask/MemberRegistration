using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace DevFramework.Core.CrossCuttingConcerns.Security.Web
{
    public class AuthenticationHelper
    {
        public static void CreateAuthCookie(Guid id, string userName, string email, string firstName, string lastName, bool rememberMe, DateTime expiration, string[] roles)
        {
            var authTicket = new FormsAuthenticationTicket(1, userName, DateTime.Now, expiration, rememberMe, CreateAuthTags(id, email, firstName, lastName, roles));
            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));
        }

        private static string CreateAuthTags(Guid id, string email, string firstName, string lastName, string[] roles)
        {
            var st = new StringBuilder();

            st.Append($"id={id}|em={email}|fi={firstName}|la={lastName}|ro=");

            for (int i = 0; i < roles.Length; i++)
            {
                st.Append(roles[i]);
                if (i < roles.Length - 1)
                    st.Append(",");
            }

            return st.ToString();
        }
    }
}
