using HospitalManagement.Data;
using HospitalManagement.Managers.Models.Domain;
using HospitalManagement.Models.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class MedicalRecordManager : IMedicalRecordManager
{
    private readonly HospitalDbContext _context;

    public MedicalRecordManager(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<List<MedicalRecord>> GetRecordsByPatientIdAsync(int patientId)
    {
        var parameter = new SqlParameter("@PatientId", patientId);

        var records = await _context.MedicalRecords
            .FromSqlRaw("EXEC GetMedicalRecordsByPatientId @PatientId", parameter)
            .ToListAsync();

        if (records == null || records.Count == 0)
        {
            throw new Exception("No medical records found for this patient.");
        }

        return records;
    }

    public async Task AddMedicalRecordAsync(MedicalRecord medicalRecord)
    {
        // Add the medical record to the database
        _context.MedicalRecords.Add(medicalRecord);
        await _context.SaveChangesAsync();
    }
}
