﻿namespace HospitalManagement.Managers.Models.DTO
{
    public class UpdatePatientRequestDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; } 
        public string? PhoneNumber { get; set; } 
    }
}
