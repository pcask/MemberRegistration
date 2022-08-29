using DevFramework.Core.Utilities.Mappings;
using MemberRegistration.Business.Abstract;
using MemberRegistration.Entities.Concrete;
using MemberRegistration.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberRegistration.MvcWebUI.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: Member
        public ActionResult Add()
        {
            return View(new MemberAddViewModel());
        }

        [HttpPost]
        public ActionResult Add(MemberAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var member = AutoMapperHelper.Map<MemberAddViewModel, Member>(model);

                _memberService.Add(member);
            }
            return View(model);
        }
    }
}