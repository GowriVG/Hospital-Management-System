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


    // POST: api/MedicalRecord
    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> AddMedicalRecord([FromBody] CreateMedicalRecordDto dto)
    {
        try
        {
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

    // GET api/medicalrecord
    [HttpGet]
    public async Task<IActionResult> GetAllMedicalRecords()
    {
        try
        {
            var records = await _medicalRecordManager.GetAllMedicalRecordsAsync();
            return Ok(records);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // DELETE: api/MedicalRecord/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedicalRecord(int id)
    {
        try
        {
            await _medicalRecordManager.DeleteMedicalRecordAsync(id);
            //return Ok($"Medical record with ID {id} deleted successfully.");
            return Ok(new { message = $"Medical record with ID {id} deleted successfully." });
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }


}
