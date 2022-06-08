using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnchTestBackend.Models
{
    public class User
    {
        [Key]
        public long Idnumber { get; set; }

        [Column("First Name")]
        public string? FirstName { get; set; }

        [Column("Last name")]
        public string? LastName { get; set; }
        public string? Email {get; set;}
        public string? Role { get; set; }
        
        [Column("Organisation Unit")]
        public string? OrganisationUnit { get; set; }

        [Column("Job title")]
        public string? JobTitle { get; set; }
    }
}
