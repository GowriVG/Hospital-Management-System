using HospitalManagement.Managers; 
using HospitalManagement.Managers.Models.DTO;
using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using System.Collections.Generic;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientManager _patientManager;

        public PatientsController(IPatientManager patientManager)
        {
            _patientManager = patientManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patientsDto = await _patientManager.GetAllPatientsAsync();
            return Ok(patientsDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var patientDto = await _patientManager.GetPatientByIdAsync(id);
            if (patientDto == null)
                return NotFound();
            return Ok(patientDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPatientRequestDto addPatientRequestDto)
        {
            var patientDto = await _patientManager.CreatePatientAsync(addPatientRequestDto);
            return CreatedAtAction(nameof(GetById), new { id = patientDto.PatientId }, patientDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePatientRequestDto updatePatientRequestDto)
        {
            var patientDto = await _patientManager.UpdatePatientAsync(id, updatePatientRequestDto);
            if (patientDto == null)
                return NotFound();
            return Ok(patientDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var patientDto = await _patientManager.DeletePatientAsync(id);
            if (patientDto == null)
                return NotFound();
            return Ok(patientDto);
        }
    }
}
