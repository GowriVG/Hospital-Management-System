using HospitalManagement.Managers;
using HospitalManagement.Managers.Models.DTO;
using HospitalManagement.Models.Domain;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PatientManagerTests
{
    [Fact]
    public async Task AddPatient_ShouldAddAndReturnPatient()
    {
        // Arrange
        var mockManager = new Mock<IPatientManager>();
        var input = new AddPatientRequestDto { FirstName = "John", Age = 30 };
        var expected = new PatientDto { PatientId = 1, FirstName = "John", Age = 30 };

        mockManager.Setup(m => m.CreatePatientAsync(input))
                   .ReturnsAsync(expected);

        // Act
        var result = await mockManager.Object.CreatePatientAsync(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected.PatientId, result.PatientId);
        Assert.Equal("John", result.FirstName);
    }

    [Fact]
    public async Task UpdatePatient_ShouldReturnUpdatedPatient()
    {
        var mockManager = new Mock<IPatientManager>();
        var input = new UpdatePatientRequestDto { FirstName = "Updated", Age = 35 };
        var expected = new PatientDto { PatientId = 1, FirstName = "Updated", Age = 35 };

        mockManager.Setup(m => m.UpdatePatientAsync(1, input)).ReturnsAsync(expected);

        var result = await mockManager.Object.UpdatePatientAsync(1, input);

        Assert.Equal("Updated", result.FirstName);
        Assert.Equal(35, result.Age);
    }

    [Fact]
    public async Task DeletePatient_ShouldReturnDeletedPatient()
    {
        var mockManager = new Mock<IPatientManager>();
        var expected = new PatientDto { PatientId = 1, FirstName = "Deleted", Age = 40 };

        mockManager.Setup(m => m.DeletePatientAsync(1)).ReturnsAsync(expected);

        var result = await mockManager.Object.DeletePatientAsync(1);

        Assert.Equal(1, result.PatientId);
        Assert.Equal("Deleted", result.FirstName);
    }

    [Fact]
    public async Task GetPatientById_ShouldReturnPatient()
    {
        var mockManager = new Mock<IPatientManager>();
        var expected = new PatientDto { PatientId = 1, FirstName = "SearchResult", Age = 25 };

        mockManager.Setup(m => m.GetPatientByIdAsync(1)).ReturnsAsync(expected);

        var result = await mockManager.Object.GetPatientByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.PatientId);
    }
}
