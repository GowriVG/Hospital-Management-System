using HospitalManagement.Managers.Models.Domain;
using HospitalManagement.Managers.Models.DTO;

namespace HospitalManagement.Managers
{
    public interface IAppointmentScheduler
    {
        // Schedule a new appointment
        Task<Appointment> ScheduleAppointmentAsync(AppointmentDto appointmentDto);

        // Update an existing appointment
        Task<Appointment> UpdateAppointmentAsync(int appointmentId, AppointmentUpdateDto updatedAppointmentDto);

        // Cancel an existing appointment
        Task<bool> CancelAppointmentAsync(int appointmentId);

        // Get appointments by patient
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);

        // Get appointments by doctor
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);

        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
    }
}
