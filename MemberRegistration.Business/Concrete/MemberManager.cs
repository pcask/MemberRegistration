using MemberRegistration.Business.Abstract;
using MemberRegistration.DataAccess.Abstract;
using MemberRegistration.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistration.Business.Concrete
{
    public class MemberManager : IMemberService
    {
        private readonly IMemberDal _memberDal;

        public MemberManager(IMemberDal memberDal)
        {
            _memberDal = memberDal;
        }

        public void Add(Member member)
        {
            _memberDal.Add(member);
        }
    }
}
