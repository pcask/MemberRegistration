using MemberRegistration.Business.KpsService;
using MemberRegistration.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistration.Business.ServiceAdapters.Kps
{
    public class KpsServiceAdapter : IKpsService
    {
        public bool Validate(Member member)
        {
            KPSPublicSoapClient client = new KPSPublicSoapClient();
            return client.TCKimlikNoDogrula(Convert.ToInt64(member.TcNo),
                                            member.FirstNane.ToUpper(),
                                            member.LastName.ToUpper(),
                                            member.DateOfBirth.Year);


        }
    }
}
