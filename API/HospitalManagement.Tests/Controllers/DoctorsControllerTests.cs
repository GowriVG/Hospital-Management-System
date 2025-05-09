using HospitalManagement.Controllers;
using HospitalManagement.Managers;
using HospitalManagement.Managers.Managers;
using HospitalManagement.Managers.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class DoctorsControllerTests
{
    private readonly Mock<IDoctorManager> _mockDoctorManager;
    private readonly DoctorsController _controller;

    public DoctorsControllerTests()
    {
        _mockDoctorManager = new Mock<IDoctorManager>();
        _controller = new DoctorsController(_mockDoctorManager.Object);
    }

    [Fact]
    public async Task GetDoctors_ShouldReturnListOfDoctors()
    {
        // Arrange
        var doctors = new List<Doctor>
        {
            new Doctor { DoctorId = 1, FirstName = "Dr. A", Specialization = "Cardiology" },
            new Doctor { DoctorId = 2, FirstName = "Dr. B", Specialization = "Neurology" }
        };
        _mockDoctorManager.Setup(m => m.GetAllDoctorsAsync()).ReturnsAsync(doctors);

        // Act
        var result = await _controller.GetDoctors();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedDoctors = Assert.IsAssignableFrom<IEnumerable<Doctor>>(okResult.Value);
        Assert.Equal(2, ((List<Doctor>)returnedDoctors).Count);
    }

    [Fact]
    public async Task GetDoctor_ExistingId_ReturnsDoctor()
    {
        // Arrange
        var doctor = new Doctor { DoctorId = 1, FirstName = "Dr. Smith", Specialization = "Dermatology" };
        _mockDoctorManager.Setup(m => m.GetDoctorByIdAsync(1)).ReturnsAsync(doctor);

        // Act
        var result = await _controller.GetDoctor(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedDoctor = Assert.IsType<Doctor>(okResult.Value);
        Assert.Equal(1, returnedDoctor.DoctorId);
    }

    [Fact]
    public async Task GetDoctor_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockDoctorManager.Setup(m => m.GetDoctorByIdAsync(99)).ReturnsAsync((Doctor)null);

        // Act
        var result = await _controller.GetDoctor(99);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task AddDoctor_ValidDoctor_ReturnsCreatedDoctor()
    {
        // Arrange
        var doctor = new Doctor { DoctorId = 3, FirstName = "Dr. New", Specialization = "Oncology" };
        _mockDoctorManager.Setup(m => m.AddDoctorAsync(doctor)).ReturnsAsync(doctor);

        // Act
        var result = await _controller.AddDoctor(doctor);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedDoctor = Assert.IsType<Doctor>(createdAtActionResult.Value);
        Assert.Equal(3, returnedDoctor.DoctorId);
    }

    [Fact]
    public async Task UpdateDoctor_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var doctor = new Doctor { DoctorId = 2, FirstName = "Mismatch", Specialization = "ENT" };

        // Act
        var result = await _controller.UpdateDoctor(3, doctor);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdateDoctor_ValidDoctor_ReturnsUpdatedDoctor()
    {
        // Arrange
        var doctor = new Doctor { DoctorId = 2, FirstName = "Updated", Specialization = "ENT" };
        _mockDoctorManager.Setup(m => m.UpdateDoctorAsync(2, doctor)).ReturnsAsync(doctor);

        // Act
        var result = await _controller.UpdateDoctor(2, doctor);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDoctor = Assert.IsType<Doctor>(okResult.Value);
        Assert.Equal("Updated", returnedDoctor.FirstName);
    }

    [Fact]
    public async Task DeleteDoctor_ExistingId_ReturnsNoContent()
    {
        // Arrange
        _mockDoctorManager.Setup(m => m.DeleteDoctorAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteDoctor(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteDoctor_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockDoctorManager.Setup(m => m.DeleteDoctorAsync(100)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteDoctor(100);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
