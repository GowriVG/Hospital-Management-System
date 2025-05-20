namespace HospitalManagement.Managers.Models.DTO
{
    public class AddDoctorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
