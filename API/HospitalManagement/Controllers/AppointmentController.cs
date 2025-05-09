using HospitalManagement.Models.Domain;
using HospitalManagement.Managers;
using Microsoft.AspNetCore.Mvc;
using HospitalManagement.Managers.Models.DTO;
using HospitalManagement.Managers.Models.Domain;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentScheduler _appointmentScheduler;

        public AppointmentController(IAppointmentScheduler appointmentScheduler)
        {
            _appointmentScheduler = appointmentScheduler;
        }

        // Schedule a new appointment
        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleAppointment([FromBody] AppointmentDto appointmentDto)
        {
            try
            {
                var scheduledAppointment = await _appointmentScheduler.ScheduleAppointmentAsync(appointmentDto);
                return CreatedAtAction(nameof(GetAppointmentById), new { appointmentId = scheduledAppointment.AppointmentId }, scheduledAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update an existing appointment
        [HttpPut("update/{appointmentId}")]
        public async Task<IActionResult> UpdateAppointment(int appointmentId, [FromBody] AppointmentUpdateDto updatedAppointmentDto)
        {
            try
            {
                var updatedAppointment = await _appointmentScheduler.UpdateAppointmentAsync(appointmentId, updatedAppointmentDto);
                return Ok(updatedAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Cancel an appointment
        [HttpDelete("cancel/{appointmentId}")]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            try
            {
                var isCancelled = await _appointmentScheduler.CancelAppointmentAsync(appointmentId);
                if (isCancelled)
                {
                    return NoContent();
                }
                return BadRequest("Failed to cancel appointment.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get appointments by patient
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetAppointmentsByPatient(int patientId)
        {
            var appointments = await _appointmentScheduler.GetAppointmentsByPatientIdAsync(patientId);
            return Ok(appointments);
        }

        // Get appointments by doctor
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsByDoctor(int doctorId)
        {
            var appointments = await _appointmentScheduler.GetAppointmentsByDoctorIdAsync(doctorId);
            return Ok(appointments);
        }

        // Get appointment by ID
        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            var appointment = await _appointmentScheduler.GetAppointmentByIdAsync(appointmentId);
            return appointment != null ? Ok(appointment) : NotFound();
        }
    }
}
