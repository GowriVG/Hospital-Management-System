using HospitalManagement.Managers.Models.Domain;
using HospitalManagement.Managers.Models.DTO;

namespace HospitalManagement.Managers
{
    public interface IAppointmentScheduler
    {
        Task<Appointment> ScheduleAppointmentAsync(AddAppointmentDto addAppointmentDto);

        Task<Appointment> UpdateAppointmentAsync(int appointmentId, AppointmentUpdateDto updatedAppointmentDto);

        // Cancel an existing appointment
        Task<bool> DeleteAppointmentAsync(int appointmentId);

        // Get appointments by patient
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);

        // Get appointments by doctor
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);

        //get all appointments 
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();

        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
    }
}
