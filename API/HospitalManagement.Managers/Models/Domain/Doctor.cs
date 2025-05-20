using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Managers.Models.Domain
{
    [Table("Doctors")]
    public class Doctor : Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
