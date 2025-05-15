using HospitalManagement.Managers;
using HospitalManagement.Managers.Managers;
using HospitalManagement.Managers.Models.Domain;
using HospitalManagement.Managers.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctorDto = await _doctorManager.GetDoctorByIdAsync(id);
            if (doctorDto == null)
                return NotFound();

            return Ok(doctorDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AddDoctorDto dto)
        {
   
            var createdDoctor = await _doctorManager.AddDoctorAsync(dto);
            return CreatedAtAction(nameof(GetDoctor), new { id = createdDoctor.DoctorId }, createdDoctor);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDoctorDto dto)
        {
            var updatedDoctor = await _doctorManager.UpdateDoctorAsync(id, dto);
            if (updatedDoctor == null)
                return NotFound();
            return Ok(updatedDoctor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _doctorManager.DeleteDoctorAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
