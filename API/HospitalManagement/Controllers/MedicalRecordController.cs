using HospitalManagement.Data;
using HospitalManagement.Managers.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using HospitalManagement.Managers.Models.DTO;
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
    public async Task<IActionResult> AddMedicalRecord([FromBody] CreateMedicalRecordDto dto)
    {
        try
        {
            var patient = await _context.Patients.FindAsync(dto.PatientId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            var doctor = await _context.Doctors.FindAsync(dto.DoctorId);
            if (doctor == null)
            {
                return BadRequest("Doctor not found.");
            }

            var medicalRecord = new MedicalRecord
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                Diagnosis = dto.Diagnosis,
                Prescription = dto.Prescription,
                TreatmentDate = dto.TreatmentDate,
                CreatedDate = DateTime.Now
            };

            await _medicalRecordManager.AddMedicalRecordAsync(medicalRecord);
            return Ok("Medical record added successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}
