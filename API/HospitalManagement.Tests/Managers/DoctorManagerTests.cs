using HospitalManagement.Managers;
using HospitalManagement.Managers.Managers;
using HospitalManagement.Managers.Models.DTO;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class DoctorManagerTests
{
    [Fact]
    public async Task AddDoctor_ShouldAddAndReturnDoctorDto()
    {
        // Arrange
        var mockManager = new Mock<IDoctorManager>();

        var input = new AddDoctorDto
        {
            FirstName = "Dr. Smith",
            LastName = "Johnson",
            Specialization = "Cardiology",
            PhoneNumber = "1234567890",
            Email = "drsmith@example.com"
        };

        var expected = new DoctorDto
        {
            DoctorId = 1,
            FirstName = "Dr. Smith",
            LastName = "Johnson",
            Specialization = "Cardiology",
            PhoneNumber = "1234567890",
            Email = "drsmith@example.com"
        };

        mockManager.Setup(m => m.AddDoctorAsync(input)).ReturnsAsync(expected);

        // Act
        var result = await mockManager.Object.AddDoctorAsync(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.DoctorId);
        Assert.Equal("Dr. Smith", result.FirstName);
    }

    [Fact]
    public async Task UpdateDoctor_ShouldReturnUpdatedDoctorDto()
    {
        // Arrange
        var mockManager = new Mock<IDoctorManager>();

        var updateDto = new UpdateDoctorDto
        {
            FirstName = "Updated Name",
            LastName = "Updated Last",
            Specialization = "Neurology",
            PhoneNumber = "9876543210",
            Email = "updated@example.com"
        };

        var expected = new DoctorDto
        {
            DoctorId = 1,
            FirstName = "Updated Name",
            LastName = "Updated Last",
            Specialization = "Neurology",
            PhoneNumber = "9876543210",
            Email = "updated@example.com"
        };

        mockManager.Setup(m => m.UpdateDoctorAsync(1, updateDto)).ReturnsAsync(expected);

        // Act
        var result = await mockManager.Object.UpdateDoctorAsync(1, updateDto);

        // Assert
        Assert.Equal("Updated Name", result.FirstName);
        Assert.Equal("Neurology", result.Specialization);
    }

    [Fact]
    public async Task DeleteDoctor_ShouldReturnDeletedDoctor()
    {
        // Arrange
        var mockManager = new Mock<IDoctorManager>();
        var expected = new DoctorDto
        {
            DoctorId = 1,
            FirstName = "Deleted",
            LastName = "Doctor",
            Specialization = "Orthopedics",
            PhoneNumber = "1234567890",
            Email = "deleted@example.com"
        };

        mockManager.Setup(m => m.DeleteDoctorAsync(1)).ReturnsAsync(expected);

        // Act
        var result = await mockManager.Object.DeleteDoctorAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.DoctorId);
        Assert.Equal("Deleted", result.FirstName);
    }


    [Fact]
    public async Task GetDoctorById_ShouldReturnDoctorDto()
    {
        // Arrange
        var mockManager = new Mock<IDoctorManager>();
        var expected = new DoctorDto
        {
            DoctorId = 1,
            FirstName = "Dr. Search",
            LastName = "Doe",
            Specialization = "Dermatology",
            PhoneNumber = "5551234567",
            Email = "search@example.com"
        };

        mockManager.Setup(m => m.GetDoctorByIdAsync(1)).ReturnsAsync(expected);

        // Act
        var result = await mockManager.Object.GetDoctorByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.DoctorId);
        Assert.Equal("Dr. Search", result.FirstName);
    }
}
