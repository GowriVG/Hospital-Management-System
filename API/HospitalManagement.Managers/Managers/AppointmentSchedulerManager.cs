using HospitalManagement.Managers.Models.Domain;
using HospitalManagement.Managers.Models.DTO;
using HospitalManagement.Managers;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Data;
using Microsoft.Data.SqlClient;

public class AppointmentSchedulerManager : IAppointmentScheduler
{
    private readonly HospitalDbContext _appointmentRepository;

    public AppointmentSchedulerManager(HospitalDbContext appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    // Schedule a new appointment
    public async Task<Appointment> ScheduleAppointmentAsync(AppointmentDto appointmentDto)
    {
        var appointment = new Appointment
        {
            PatientId = appointmentDto.PatientId,
            DoctorId = appointmentDto.DoctorId,
            AppointmentDate = appointmentDto.AppointmentDate,
            Status = appointmentDto.Status,
            Reason = appointmentDto.Reason,
            CreatedDate = DateTime.UtcNow
        };

        await _appointmentRepository.AddAppointmentAsync(appointment);
        return appointment;
    }

    // Update an existing appointment
    public async Task<Appointment> UpdateAppointmentAsync(int appointmentId, AppointmentUpdateDto updatedAppointmentDto)
    {
        var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
        if (existingAppointment == null)
        {
            throw new Exception("Appointment not found");
        }

        existingAppointment.PatientId = updatedAppointmentDto.PatientId;
        existingAppointment.DoctorId = updatedAppointmentDto.DoctorId;
        existingAppointment.AppointmentDate = updatedAppointmentDto.AppointmentDate;
        existingAppointment.Status = updatedAppointmentDto.Status;
        existingAppointment.Reason = updatedAppointmentDto.Reason;

        await _appointmentRepository.UpdateAppointmentAsync(existingAppointment);
        return existingAppointment;
    }

    // Cancel an existing appointment
    public async Task<bool> CancelAppointmentAsync(int appointmentId)
    {
        var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
        if (existingAppointment == null)
        {
            throw new Exception("Appointment not found");
        }

        // Update the status to 'Canceled'
        existingAppointment.Status = AppointmentStatus.Canceled;
        await _appointmentRepository.UpdateAppointmentAsync(existingAppointment);
        return true;
    }

    // Get all appointments by a specific patient
    public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
    {
        var parameter = new SqlParameter("@PatientId", patientId);

        var appointments = await _appointmentRepository.Appointments
            .FromSqlRaw("EXEC GetAppointmentsByPatientId @PatientId", parameter)
            .ToListAsync();

        if (appointments == null || !appointments.Any())
        {
            throw new Exception("No appointments found for this patient.");
        }

        return appointments;
    }

    // Get all appointments by a specific doctor
    public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
    {
        var parameter = new SqlParameter("@DoctorId", doctorId);

        var appointments = await _appointmentRepository.Appointments
            .FromSqlRaw("EXEC GetAppointmentsByDoctorId @DoctorId", parameter)
            .ToListAsync();

        if (appointments == null || !appointments.Any())
        {
            throw new Exception("No appointments found for this doctor.");
        }

        return appointments;
    }

    // Get appointment by its ID
    public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
    {
        var parameter = new SqlParameter("@AppointmentId", appointmentId);

        var appointments = await _appointmentRepository.Appointments
            .FromSqlRaw("EXEC GetAppointmentById @AppointmentId", parameter)
            .ToListAsync();

        var appointment = appointments.FirstOrDefault();

        if (appointment == null)
        {
            throw new Exception("Appointment not found.");
        }

        return appointment;
    }
}
