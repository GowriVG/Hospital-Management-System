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


    public async Task<List<MedicalRecord>> GetAllMedicalRecordsAsync()
    {
        var records = await _context.MedicalRecords
         .Where(r => !r.IsDeleted)
         .ToListAsync();

        if (records == null || records.Count == 0)
        {
            throw new Exception("No medical records found.");
        }

        return records;
    }


    public async Task AddMedicalRecordAsync(MedicalRecord medicalRecord)
    {
        var patientExists = await _context.Patients
            .AnyAsync(p => p.PatientId == medicalRecord.PatientId && !p.IsDeleted);
        if (!patientExists)
        {
            throw new Exception($"Patient with ID {medicalRecord.PatientId} does not exist or is deleted.");
        }

        var doctorExists = await _context.Doctors
            .AnyAsync(d => d.DoctorId == medicalRecord.DoctorId && !d.IsDeleted);
        if (!doctorExists)
        {
            throw new Exception($"Doctor with ID {medicalRecord.DoctorId} does not exist or is deleted.");
        }

        _context.MedicalRecords.Add(medicalRecord);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteMedicalRecordAsync(int recordId)
    {
        var record = await _context.MedicalRecords.FirstOrDefaultAsync(r => r.RecordId == recordId);

        if (record == null)
        {
            throw new Exception($"Medical record with ID {recordId} not found.");
        }

        if (record.IsDeleted)
        {
            throw new Exception($"Medical record with ID {recordId} is already deleted.");
        }

        // Soft delete: mark the record as deleted
        record.IsDeleted = true;

        _context.MedicalRecords.Update(record);
        await _context.SaveChangesAsync();
    }

}
