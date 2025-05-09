using HospitalManagement.Managers;
using HospitalManagement.Managers.Managers;
using HospitalManagement.Managers.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorManager _doctorManager;
        public DoctorsController(IDoctorManager doctorManager)
        {
            _doctorManager = doctorManager;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            var doctors = await _doctorManager.GetAllDoctorsAsync();
            return Ok(doctors);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _doctorManager.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound();
            return Ok(doctor);
        }
        [HttpPost]
        public async Task<ActionResult<Doctor>> AddDoctor(Doctor doctor)
        {
            var createdDoctor = await _doctorManager.AddDoctorAsync(doctor);
            return CreatedAtAction(nameof(GetDoctor), new { id = createdDoctor.DoctorId }, createdDoctor);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, Doctor doctor)
        {
            if (id != doctor.DoctorId)
                return BadRequest();
            var updatedDoctor = await _doctorManager.UpdateDoctorAsync(id,doctor);
            return Ok(updatedDoctor);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _doctorManager.DeleteDoctorAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
