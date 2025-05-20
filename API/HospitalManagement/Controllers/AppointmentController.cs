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
        [HttpPost("add")]
        public async Task<IActionResult> ScheduleAppointment([FromBody] AddAppointmentDto addAppointmentDto)
        {
            try
            {
                var scheduledAppointment = await _appointmentScheduler.ScheduleAppointmentAsync(addAppointmentDto);
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
        [HttpDelete("delete/{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment(int appointmentId)
        {
            try
            {
                var result = await _appointmentScheduler.DeleteAppointmentAsync(appointmentId);
                if (result)
                {
                    return Ok(new { message = "Appointment deleted successfully." });
                }
                return BadRequest("Failed to delete appointment.");
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

        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            try
            {
                var appointment = await _appointmentScheduler.GetAppointmentByIdAsync(appointmentId);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task <IActionResult> GetAllAppointmentsAsync()
        {
            var AppointmentDto = await _appointmentScheduler.GetAllAppointmentsAsync();
            return Ok(AppointmentDto);
        }
    }
}
