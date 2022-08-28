using MemberRegistration.Business.Abstract;
using MemberRegistration.Business.DependencyResolvers.Ninject;
using MemberRegistration.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistration.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IMemberService memberService = InstanceFactory.GetInstance<IMemberService>();
            Member mem = new Member()
            {
                TcNo = "xxx",
                DateOfBirth = new DateTime(1994, 05, 11),
                Email = "sezer.ayran@gmail.com",
                FirstName = "sezer",
                LastName = "ayran"
            };

            memberService.Add(mem);

            Console.WriteLine("The member has been successfully added.");
            Console.ReadLine();
        }
    }
}
