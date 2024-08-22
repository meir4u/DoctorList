using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.BL.DTOs
{
    public class ContactUsDto
    {
        [Required]
        [MinLength(3)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{3}-\d{7}$", ErrorMessage = "Phone number must be in the format XXX-XXXXXXX.")]
        public  string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public  string Email { get; set; }
    }
}
