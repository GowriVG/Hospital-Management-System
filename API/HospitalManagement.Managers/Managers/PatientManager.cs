using HospitalManagement.Data;
using HospitalManagement.Models.Domain;
using HospitalManagement.Managers.Models.DTO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Managers
{
    public class PatientManager : IPatientManager
    {
        private readonly HospitalDbContext _context;

        public PatientManager(HospitalDbContext context)
        {
            _context = context;
        }

        // Calculate age based on DateOfBirth
        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;  // Adjust age if birthday hasn't occurred yet this year
            return age;
        }

        public async Task<List<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            return patients.Select(p => new PatientDto
            {
                PatientId = p.PatientId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Age = p.Age,
                Gender = p.Gender,
                Address = p.Address,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber
            }).ToList();
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(x => x.PatientId == id && !x.IsDeleted);

            if (patient == null) return null;

            return new PatientDto
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Age = patient.Age,
                Gender = patient.Gender,
                Address = patient.Address,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber
            };
        }

        public async Task<PatientDto> CreatePatientAsync(AddPatientRequestDto addPatientRequestDto)
        {
            var patient = new Patient
            {
                FirstName = addPatientRequestDto.FirstName,
                LastName = addPatientRequestDto.LastName,
                DateOfBirth = addPatientRequestDto.DateOfBirth,
                Age = CalculateAge(addPatientRequestDto.DateOfBirth), // Calculate age based on DOB
                Gender = addPatientRequestDto.Gender,
                Address = addPatientRequestDto.Address,
                PhoneNumber = addPatientRequestDto.PhoneNumber,
                Email = addPatientRequestDto.Email,
                IsDeleted = false // default value
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return new PatientDto
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Age = patient.Age,
                Gender = patient.Gender,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email
            };
        }

        public async Task<PatientDto> UpdatePatientAsync(int id, UpdatePatientRequestDto updatePatientRequestDto)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(x => x.PatientId == id && !x.IsDeleted);

            if (patient == null) return null;

            // Update only fields that are provided (not null)
            if (!string.IsNullOrWhiteSpace(updatePatientRequestDto.FirstName))
                patient.FirstName = updatePatientRequestDto.FirstName;

            if (!string.IsNullOrWhiteSpace(updatePatientRequestDto.LastName))
                patient.LastName = updatePatientRequestDto.LastName;

            if (updatePatientRequestDto.DateOfBirth.HasValue)
            {
                patient.DateOfBirth = updatePatientRequestDto.DateOfBirth.Value;
                patient.Age = CalculateAge(patient.DateOfBirth);  // Recalculate age only if DOB is updated
            }

            if (!string.IsNullOrWhiteSpace(updatePatientRequestDto.Gender))
                patient.Gender = updatePatientRequestDto.Gender;

            if (!string.IsNullOrWhiteSpace(updatePatientRequestDto.Address))
                patient.Address = updatePatientRequestDto.Address;

            if (!string.IsNullOrWhiteSpace(updatePatientRequestDto.Email))
                patient.Email = updatePatientRequestDto.Email;

            if (!string.IsNullOrWhiteSpace(updatePatientRequestDto.PhoneNumber))
                patient.PhoneNumber = updatePatientRequestDto.PhoneNumber;

            await _context.SaveChangesAsync();

            return new PatientDto
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Age = patient.Age,
                Gender = patient.Gender,
                Address = patient.Address,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber
            };
        }


        public async Task<PatientDto> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(x => x.PatientId == id && !x.IsDeleted);

            if (patient == null) return null;

            patient.IsDeleted = true;
            await _context.SaveChangesAsync();

            return new PatientDto
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Address = patient.Address,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber
            };
        }
    }
}
