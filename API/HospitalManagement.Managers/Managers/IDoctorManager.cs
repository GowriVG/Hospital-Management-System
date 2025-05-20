using HospitalManagement.Managers.Models.Domain;
using HospitalManagement.Managers.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Managers.Managers
{
    public interface IDoctorManager
    {
        Task<List<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto> GetDoctorByIdAsync(int id);
        Task<DoctorDto> AddDoctorAsync(AddDoctorDto dto);
        Task<DoctorDto> UpdateDoctorAsync(int id, UpdateDoctorDto dto);
        Task<DoctorDto> DeleteDoctorAsync(int id);
    }
}
