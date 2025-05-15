using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Managers.Models.Domain;
using HospitalManagement.Managers.Models.DTO;

namespace HospitalManagement.Managers.Models.DTO
{
    public class AppointmentUpdateDto
    {
        //public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
        public string Reason { get; set; }
    }
}
