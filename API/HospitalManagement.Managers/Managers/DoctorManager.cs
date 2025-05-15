using HospitalManagement.Data;
using HospitalManagement.Managers.Managers;
using HospitalManagement.Managers.Models.Domain;
using HospitalManagement.Managers.Models.DTO;
using HospitalManagement.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HospitalManagement.Managers
{
    public class DoctorManager : IDoctorManager
    {
        private readonly HospitalDbContext _context;
        public DoctorManager(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors
                .Where(d => !d.IsDeleted)
                .ToListAsync();

            return doctors.Select(d => new DoctorDto
            {
                DoctorId = d.DoctorId,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Specialization = d.Specialization,
                PhoneNumber = d.PhoneNumber,
                Email = d.Email
            }).ToList();
        }

        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(x => x.DoctorId == id && !x.IsDeleted);

            if (doctor == null) return null;

            return new DoctorDto
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email
            };
        }


        public async Task<DoctorDto> AddDoctorAsync(AddDoctorDto dto)
        {
            var doctor = new Doctor
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Specialization = dto.Specialization,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                IsDeleted = false
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return new DoctorDto
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email
            };
        }

        public async Task<DoctorDto> UpdateDoctorAsync(int id, UpdateDoctorDto dto)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == id && !d.IsDeleted);

            if (doctor == null) return null;

            // Update only fields that are provided (not null or whitespace)
            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                doctor.FirstName = dto.FirstName;

            if (!string.IsNullOrWhiteSpace(dto.LastName))
                doctor.LastName = dto.LastName;

            if (!string.IsNullOrWhiteSpace(dto.Specialization))
                doctor.Specialization = dto.Specialization;

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
                doctor.PhoneNumber = dto.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                doctor.Email = dto.Email;

            // Add more field checks here if UpdateDoctorDto includes more fields

            await _context.SaveChangesAsync();

            return new DoctorDto
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email
            };
        }

        public async Task<DoctorDto> DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == id && !d.IsDeleted);

            if (doctor == null) return null;

            doctor.IsDeleted = true;
            await _context.SaveChangesAsync();

            return new DoctorDto
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email
            };
        }

    }
}
