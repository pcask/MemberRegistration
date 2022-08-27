using DevFramework.Core.Aspects.PostSharp.ValidationAspects;
using MemberRegistration.Business.Abstract;
using MemberRegistration.Business.ServiceAdapters.Kps;
using MemberRegistration.Business.ValidationRules.FluentValidation;
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
        private readonly IKpsService _kpsService;

        public MemberManager(IMemberDal memberDal, IKpsService kpsService)
        {
            _memberDal = memberDal;
            _kpsService = kpsService;
        }

        [FluentValidationAspect(typeof(MemberValidator))]
        public void Add(Member member)
        {
            if (_kpsService.Validate(member) == false)
                throw new Exception("Kullanıcı bilgileri hatalı!");

            _memberDal.Add(member);
        }
    }
}
