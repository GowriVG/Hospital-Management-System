using HospitalManagement.Managers;
using HospitalManagement.Managers.Managers;
using HospitalManagement.Managers.Models.Domain;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class DoctorManagerTests
{
    [Fact]
    public async Task AddDoctor_ShouldAddAndReturnDoctor()
    {
        // Arrange
        var mockManager = new Mock<IDoctorManager>();
        var input = new Doctor { DoctorId = 1, FirstName = "Dr. Smith", Specialization = "Cardiology" };
        var expected = input;

        mockManager.Setup(m => m.AddDoctorAsync(input)).ReturnsAsync(expected);

        // Act
        var result = await mockManager.Object.AddDoctorAsync(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.DoctorId);
        Assert.Equal("Dr. Smith", result.FirstName);
    }

    [Fact]
    public async Task UpdateDoctor_ShouldReturnUpdatedDoctor()
    {
        // Arrange
        var mockManager = new Mock<IDoctorManager>();
        var updatedDoctor = new Doctor { DoctorId = 1, FirstName = "Updated Name", Specialization = "Neurology" };

        mockManager.Setup(m => m.UpdateDoctorAsync(1, updatedDoctor)).ReturnsAsync(updatedDoctor);

        // Act
        var result = await mockManager.Object.UpdateDoctorAsync(1, updatedDoctor);

        // Assert
        Assert.Equal("Updated Name", result.FirstName);
        Assert.Equal("Neurology", result.Specialization);
    }

    [Fact]
    public async Task DeleteDoctor_ShouldReturnTrue_WhenDoctorExists()
    {
        // Arrange
        var mockManager = new Mock<IDoctorManager>();

        mockManager.Setup(m => m.DeleteDoctorAsync(1)).ReturnsAsync(true);

        // Act
        var result = await mockManager.Object.DeleteDoctorAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetDoctorById_ShouldReturnDoctor()
    {
        // Arrange
        var mockManager = new Mock<IDoctorManager>();
        var expected = new Doctor { DoctorId = 1, FirstName = "Dr. Search", Specialization = "Dermatology" };

        mockManager.Setup(m => m.GetDoctorByIdAsync(1)).ReturnsAsync(expected);

        // Act
        var result = await mockManager.Object.GetDoctorByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.DoctorId);
        Assert.Equal("Dr. Search", result.FirstName);
    }
}
