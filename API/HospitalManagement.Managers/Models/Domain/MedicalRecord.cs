using HospitalManagement.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Managers.Models.Domain
{
    public class MedicalRecord
    {
        [Key]
        public int RecordId { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        public string Prescription { get; set; }

        [Required]
        public DateTime TreatmentDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation Properties (Virtual allows lazy loading or eager loading)
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }

    }
}
