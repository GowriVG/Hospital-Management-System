

namespace HospitalManagement.Managers.Models.DTO
{
    public class CreateMedicalRecordDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public DateTime TreatmentDate { get; set; }
    }
}
