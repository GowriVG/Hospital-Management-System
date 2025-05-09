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

        public async Task<List<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            return patients.Select(p => new PatientDto
            {
                PatientId = p.PatientId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Age = p.Age,
                Gender = p.Gender,
                Address = p.Address,
                PhoneNumber = p.PhoneNumber
            }).ToList();
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.PatientId == id);
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
                Age = addPatientRequestDto.Age,
                Gender = addPatientRequestDto.Gender,
                Address = addPatientRequestDto.Address,
                PhoneNumber = addPatientRequestDto.PhoneNumber
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
                PhoneNumber = patient.PhoneNumber
            };
        }

        public async Task<PatientDto> UpdatePatientAsync(int id, UpdatePatientRequestDto updatePatientRequestDto)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.PatientId == id);
            if (patient == null) return null;

            patient.FirstName = updatePatientRequestDto.FirstName;
            patient.LastName = updatePatientRequestDto.LastName;
            patient.DateOfBirth = updatePatientRequestDto.DateOfBirth;
            patient.Gender = updatePatientRequestDto.Gender;
            patient.Address = updatePatientRequestDto.Address;
            patient.PhoneNumber = updatePatientRequestDto.PhoneNumber;

            await _context.SaveChangesAsync();

            return new PatientDto
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber
            };
        }

        public async Task<PatientDto> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.PatientId == id);
            if (patient == null) return null;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return new PatientDto
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber
            };
        }
    }
}
