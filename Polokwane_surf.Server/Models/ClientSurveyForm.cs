//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//public class ClientSurveyForm
//{

//        public int Id { get; set; } // optional, for EF
//        public string Name { get; set; }
//        public string Email { get; set; }
//        public string ServiceUsed { get; set; }
//        public string Feedback { get; set; }
//        public string Rating { get; set; } // or int if numeric
//        public DateTime SubmittedAt { get; set; } // will be filled in server-side


//}

using System;
using System.ComponentModel.DataAnnotations;

namespace Polokwane_surf.Server.Models
{
    public class ClientSurvey
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? ServiceUsed { get; set; }

        [Required]
        public int? Rating { get; set; }

        [Required]
        public string? Feedback { get; set; }

        public DateTime SubmittedAt { get; set; }
    }
}
