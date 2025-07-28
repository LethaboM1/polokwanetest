//using System.ComponentModel.DataAnnotations;
//using System.Text.Json.Serialization;

//namespace Polokwane_surf.Server.Models
//{
//    public class ContactForm
//    {
//        [Required]
//        public string Name { get; set; }

//        [Required]
//        [EmailAddress]
//        public string Email { get; set; }

//        //[Required]
//        public string PhoneNumber { get; set; }

//        [Required]
//        public string Subject { get; set; }

//        [Required]
//        public string Message { get; set; }
//    }


//}


using System;
using System.ComponentModel.DataAnnotations;

namespace Polokwane_surf.Server.Models
{
    public class ContactForm
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime SubmittedAt { get; set; }
    }
}
