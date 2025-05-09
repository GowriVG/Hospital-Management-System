using HospitalManagement.Data;
using HospitalManagement.Managers.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class MedicalRecordController : ControllerBase
{
    private readonly IMedicalRecordManager _medicalRecordManager;
    private readonly HospitalDbContext _context;

    public MedicalRecordController(IMedicalRecordManager medicalRecordManager, HospitalDbContext context)
    {
        _medicalRecordManager = medicalRecordManager;
        _context = context;
    }

    // GET api/medicalrecord/patient/{patientId}
    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetRecordsByPatientId(int patientId)
    {
        try
        {
            var records = await _medicalRecordManager.GetRecordsByPatientIdAsync(patientId);
            return Ok(records);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // POST: api/MedicalRecord
    [HttpPost]
    public async Task<IActionResult> AddMedicalRecord([FromBody] MedicalRecord medicalRecord)
    {
        try
        {
            // Ensure that patientId and doctorId are valid
            var patient = await _context.Patients.FindAsync(medicalRecord.PatientId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            var doctor = await _context.Doctors.FindAsync(medicalRecord.DoctorId);
            if (doctor == null)
            {
                return BadRequest("Doctor not found.");
            }

            // Add the medical record
            await _medicalRecordManager.AddMedicalRecordAsync(medicalRecord);

            return Ok("Medical record added successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}
