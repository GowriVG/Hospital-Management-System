using Microsoft.EntityFrameworkCore;
using HospitalManagement.Models.Domain;
using HospitalManagement.Managers.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().ToTable("Doctors");

            modelBuilder.Entity<Patient>().ToTable("Patients");

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion<string>();

            // MedicalRecord relationships
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(r => r.Patient)
                .WithMany()
                .HasForeignKey(r => r.PatientId);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(r => r.Doctor)
                .WithMany()
                .HasForeignKey(r => r.DoctorId);

            base.OnModelCreating(modelBuilder);
        }

        // Custom method to get an appointment by ID
        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            return await Appointments.FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
        }

        // Custom method to get appointments by patient ID
        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return await Appointments.Where(a => a.PatientId == patientId).ToListAsync();
        }

        // Custom method to get appointments by doctor ID
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await Appointments.Where(a => a.DoctorId == doctorId).ToListAsync();
        }

        // Method to add a new appointment
        public async Task AddAppointmentAsync(Appointment appointment)
        {
            await Appointments.AddAsync(appointment);
            await SaveChangesAsync(); // Make sure to save changes after adding the appointment
        }

        // Method to update an existing appointment
        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            Appointments.Update(appointment);
            await SaveChangesAsync(); // Make sure to save changes after updating the appointment
        }
    }
}
