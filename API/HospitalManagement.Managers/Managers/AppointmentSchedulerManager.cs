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
    public async Task<Appointment> ScheduleAppointmentAsync(AddAppointmentDto addappointmentDto)
    {
        // Check if patient exists and is not deleted
        var patientExists = await _appointmentRepository.Patients
            .AnyAsync(p => p.PatientId == addappointmentDto.PatientId && !p.IsDeleted);
        if (!patientExists)
        {
            throw new Exception("Cannot schedule appointment: Patient not found or has been deleted.");
        }

        // Check if doctor exists and is not deleted
        var doctorExists = await _appointmentRepository.Doctors
            .AnyAsync(d => d.DoctorId == addappointmentDto.DoctorId && !d.IsDeleted);
        if (!doctorExists)
        {
            throw new Exception("Cannot schedule appointment: Doctor not found or has been deleted.");
        }

        if (!Enum.TryParse<AppointmentStatus>(addappointmentDto.Status, true, out var status))
        {
            throw new Exception("Invalid appointment status value.");
        }

        var appointment = new Appointment
        {
            PatientId = addappointmentDto.PatientId,
            DoctorId = addappointmentDto.DoctorId,
            AppointmentDate = addappointmentDto.AppointmentDate,
            Status = status,
            Reason = addappointmentDto.Reason,
            CreatedDate = DateTime.UtcNow
        };

        await _appointmentRepository.AddAppointmentAsync(appointment);
        return appointment;
    }

    // Update an existing appointment
    public async Task<Appointment> UpdateAppointmentAsync(int appointmentId, AppointmentUpdateDto updatedAppointmentDto)
    {
        var existingAppointment = await _appointmentRepository.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == appointmentId && !a.IsDeleted);

        if (existingAppointment == null)
        {
            throw new Exception("Appointment not found or has been deleted.");
        }


        if (!Enum.TryParse<AppointmentStatus>(updatedAppointmentDto.Status, true, out var status))
        {
            throw new Exception("Invalid appointment status value.");
        }



        existingAppointment.PatientId = updatedAppointmentDto.PatientId;
        existingAppointment.DoctorId = updatedAppointmentDto.DoctorId;
        existingAppointment.AppointmentDate = updatedAppointmentDto.AppointmentDate;
        existingAppointment.Status = status;
        existingAppointment.Reason = updatedAppointmentDto.Reason;

        await _appointmentRepository.UpdateAppointmentAsync(existingAppointment);
        return existingAppointment;
    }

    // Soft delete an existing appointment (mark as deleted)
    public async Task<bool> DeleteAppointmentAsync(int appointmentId)
    {
        var existingAppointment = await _appointmentRepository.Appointments
            .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

        if (existingAppointment == null || existingAppointment.IsDeleted)
        {
            throw new Exception("Appointment not found or already deleted");
        }

        // Mark the appointment as deleted
        existingAppointment.IsDeleted = true;

        // Optionally, update status
        // existingAppointment.Status = AppointmentStatus.Deleted;

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

        var appointment = appointments.FirstOrDefault(a => !a.IsDeleted);

        foreach (var a in appointments)
        {
            Console.WriteLine($"ID: {a.AppointmentId}, IsDeleted: {a.IsDeleted}");
        }

        if (appointment == null)
        {
            throw new Exception("Appointment not found.");
        }

        return appointment;
    }
    public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
    {
        var appointments = await _appointmentRepository.Appointments.Where(a => !a.IsDeleted).ToListAsync();

        // Map each Appointment to AppointmentDto
        var appointmentDtos = appointments.Select(a => new AppointmentDto
        {
            AppointmentId = a.AppointmentId,
            PatientId = a.PatientId,
            DoctorId = a.DoctorId,
            AppointmentDate = a.AppointmentDate,
            Status = a.Status,
            Reason = a.Reason
        });

        return appointmentDtos;
    }

}
