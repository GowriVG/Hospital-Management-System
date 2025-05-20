using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Managers.Models.Domain
{
    public abstract class Person
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MaxLength(15)]
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
