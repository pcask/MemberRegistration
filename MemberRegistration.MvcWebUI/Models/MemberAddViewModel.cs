using FluentValidation.Attributes;
using MemberRegistration.Business.ValidationRules.FluentValidation;
using MemberRegistration.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistration.MvcWebUI.Models
{
    public class MemberAddViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(11, ErrorMessage = "TC Identity Number must be 11 characters.", MinimumLength = 11)]
        public string TcNo { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
