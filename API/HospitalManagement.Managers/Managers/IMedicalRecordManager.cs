using HospitalManagement.Managers.Models.Domain;

public interface IMedicalRecordManager
{
    Task<List<MedicalRecord>> GetRecordsByPatientIdAsync(int patientId);
    Task AddMedicalRecordAsync(MedicalRecord medicalRecord);
}
