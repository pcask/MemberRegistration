using MemberRegistration.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistration.Business.ServiceAdapters.Kps
{
    public interface IKpsService
    {
        bool Validate(Member member);
    }
}
