using HospitalManagement.Managers.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalManagement.Managers
{
    public interface IPatientManager
    {
        Task<List<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto> GetPatientByIdAsync(int id);
        Task<PatientDto> CreatePatientAsync(AddPatientRequestDto addPatientRequestDto);
        Task<PatientDto> UpdatePatientAsync(int id, UpdatePatientRequestDto updatePatientRequestDto);
        Task<PatientDto> DeletePatientAsync(int id);
        //Task<PatientDto> PatchPatientAsync(int id, PatientDto patchedPatient);
        //Task<PatientDto> PatchPatientAsync(int id, JsonPatchDocument<PatientDto> patchDoc);

    }
}
