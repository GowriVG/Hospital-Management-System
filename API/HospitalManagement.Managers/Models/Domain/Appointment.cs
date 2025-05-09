using HospitalManagement.Models.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Managers.Models.Domain
{
    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Canceled
    }

    [Table("Appointment")]
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        public string Reason { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Doctor Doctor { get; set; }

        public Patient Patient { get; set; }
    }
}
