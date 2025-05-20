using HospitalManagement.Managers.Models.Domain;

public interface IMedicalRecordManager
{
    //Task<List<MedicalRecord>> GetRecordsByPatientIdAsync(int patientId);
    Task<List<MedicalRecord>> GetAllMedicalRecordsAsync();

    Task AddMedicalRecordAsync(MedicalRecord medicalRecord);
    Task DeleteMedicalRecordAsync(int recordId); 

}
