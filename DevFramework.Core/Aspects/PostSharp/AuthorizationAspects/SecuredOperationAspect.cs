using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.AuthorizationAspects
{
    [Serializable]
    public class SecuredOperationAspect : OnMethodBoundaryAspect
    {
        public string Roles { get; set; }

        public override void OnEntry(MethodExecutionArgs args)
        {
            bool IsAuthorized = false;

            if (String.IsNullOrWhiteSpace(Roles) == false)
            {
                //                                          İki virgül arasında Empty değer söz konusuysa onları silerek diziye göndermez. 
                var roles = Roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.ToLower().Trim());

                foreach (var role in roles)
                {
                    if (Thread.CurrentPrincipal.IsInRole(role))
                        IsAuthorized = true;
                }

                if (IsAuthorized == false)
                    throw new SecurityException("You are not authorized!");
            }
            else
                throw new Exception("Roles must be specified!");

            base.OnEntry(args);
        }
    }
}
