using HospitalManagement.Managers.Models.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Models.Domain
{
    [Table("Patients")]
    public class Patient : Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }


        [Required]
        [RegularExpression("M|F|O", ErrorMessage = "Gender must be 'M', 'F', or 'O'.")]
        public string Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        public bool IsDeleted { get; set; } = false; // Soft delete flag
    }
}
