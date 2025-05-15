using HospitalManagement.Controllers;
using HospitalManagement.Managers;
using HospitalManagement.Managers.Managers;
using HospitalManagement.Managers.Models.Domain;
using HospitalManagement.Managers.Models.DTO;
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
        var doctorDtos = new List<DoctorDto> {
    new DoctorDto { DoctorId = 1, FirstName = "Dr. A", Specialization = "Cardiology" },
    new DoctorDto { DoctorId = 2, FirstName = "Dr. B", Specialization = "Neurology" }
};
        _mockDoctorManager.Setup(m => m.GetAllDoctorsAsync()).ReturnsAsync(doctorDtos);


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
        var doctorDto = new DoctorDto { DoctorId = 1, FirstName = "Dr. Smith", Specialization = "Dermatology" };
        _mockDoctorManager.Setup(m => m.GetDoctorByIdAsync(1)).ReturnsAsync(doctorDto);

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
        _mockDoctorManager.Setup(m => m.GetDoctorByIdAsync(99)).ReturnsAsync((DoctorDto)null);

        // Act
        var result = await _controller.GetDoctor(99);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task AddDoctor_ValidDoctor_ReturnsCreatedDoctor()
    {
        // Arrange
        var doctor = new AddDoctorDto { FirstName = "Dr. New", Specialization = "Oncology" };
        var createdDoctor = new DoctorDto { DoctorId = 3, FirstName = "Dr. New", Specialization = "Oncology" };

        _mockDoctorManager.Setup(m => m.AddDoctorAsync(doctor)).ReturnsAsync(createdDoctor);

        // Act
        var result = await _controller.Create(doctor);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

        var returnedDoctor = Assert.IsType<DoctorDto>(createdAtActionResult.Value);
        Assert.Equal(3, returnedDoctor.DoctorId);
        Assert.Equal("Dr. New", returnedDoctor.FirstName);
    }

    [Fact]
    public async Task UpdateDoctor_IdMismatch_ReturnsBadRequest()
    {
        var updateDto = new UpdateDoctorDto { FirstName = "Mismatch", Specialization = "ENT" };
        var result = await _controller.Update(3, updateDto);
        Assert.IsType<BadRequestResult>(result);


        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdateDoctor_ValidDoctor_ReturnsUpdatedDoctor()
    {
        var updateDto = new UpdateDoctorDto { FirstName = "Updated", Specialization = "ENT" };
        var updatedDoctor = new DoctorDto { DoctorId = 2, FirstName = "Updated", Specialization = "ENT" };

        _mockDoctorManager.Setup(m => m.UpdateDoctorAsync(2, updateDto)).ReturnsAsync(updatedDoctor);

        var result = await _controller.Update(2, updateDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDoctor = Assert.IsType<DoctorDto>(okResult.Value);
        Assert.Equal("Updated", returnedDoctor.FirstName);
        Assert.Equal("ENT", returnedDoctor.Specialization);
    }

    [Fact]
    public async Task DeleteDoctor_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var deletedDoctor = new DoctorDto { DoctorId = 1, FirstName = "Dr. Removed", Specialization = "Cardiology" };
        _mockDoctorManager.Setup(m => m.DeleteDoctorAsync(1)).ReturnsAsync(deletedDoctor);

        // Act
        var result = await _controller.DeleteDoctor(1);

        // Assert
        Assert.IsType<NoContentResult>(result);

    }

    [Fact]
    public async Task DeleteDoctor_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockDoctorManager.Setup(m => m.DeleteDoctorAsync(100)).ReturnsAsync((DoctorDto)null);

        // Act
        var result = await _controller.DeleteDoctor(100);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
