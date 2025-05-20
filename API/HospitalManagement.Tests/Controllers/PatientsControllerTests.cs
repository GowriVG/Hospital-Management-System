using HospitalManagement.Controllers;
using HospitalManagement.Managers;
using HospitalManagement.Managers.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HospitalManagement.Tests
{
    public class PatientsControllerTests
    {
        private readonly Mock<IPatientManager> _mockPatientManager;
        private readonly PatientsController _controller;

        public PatientsControllerTests()
        {
            _mockPatientManager = new Mock<IPatientManager>();
            _controller = new PatientsController(_mockPatientManager.Object);
        }

        // Test GetAllPatients method
        [Fact]
        public async Task GetAllPatients_ReturnsOkResult_WithListOfPatients()
        {
            // Arrange
            var mockPatients = new List<PatientDto>
            {
                new PatientDto { PatientId = 1, FirstName = "John Doe", Age = 30 },
                new PatientDto { PatientId = 2, FirstName = "Jane Doe", Age = 28 }
            };
            _mockPatientManager.Setup(m => m.GetAllPatientsAsync()).ReturnsAsync(mockPatients);

            // Act
            var result = await _controller.GetAllPatients();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<PatientDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        // Test GetById method
        [Fact]
        public async Task GetById_ReturnsOkResult_WithPatient()
        {
            // Arrange
            var mockPatient = new PatientDto { PatientId = 1, FirstName = "John Doe", Age = 30 };
            _mockPatientManager.Setup(m => m.GetPatientByIdAsync(1)).ReturnsAsync(mockPatient);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(1, returnValue.PatientId);
            Assert.Equal("John Doe", returnValue.FirstName);
        }

        [Fact]
        public async Task GetById_ReturnsNotFoundResult_WhenPatientDoesNotExist()
        {
            // Arrange
            _mockPatientManager.Setup(m => m.GetPatientByIdAsync(99)).ReturnsAsync((PatientDto)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test Create method
        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult_WithPatient()
        {
            // Arrange
            var newPatient = new AddPatientRequestDto { FirstName = "John Doe"};
            var createdPatient = new PatientDto { PatientId = 1, FirstName = "John Doe", Age = 30 };
            _mockPatientManager.Setup(m => m.CreatePatientAsync(It.IsAny<AddPatientRequestDto>())).ReturnsAsync(createdPatient);

            // Act
            var result = await _controller.Create(newPatient);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<PatientDto>(createdAtActionResult.Value);
            Assert.Equal(1, returnValue.PatientId);
            Assert.Equal("John Doe", returnValue.FirstName);
        }

        // Test Update method
        [Fact]
        public async Task Update_ReturnsOkResult_WithUpdatedPatient()
        {
            // Arrange
            var updatePatient = new UpdatePatientRequestDto { FirstName = "John Updated"};
            var updatedPatient = new PatientDto { PatientId = 1, FirstName = "John Updated", Age = 35 };
            _mockPatientManager.Setup(m => m.UpdatePatientAsync(1, It.IsAny<UpdatePatientRequestDto>())).ReturnsAsync(updatedPatient);

            // Act
            var result = await _controller.Update(1, updatePatient);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(1, returnValue.PatientId);
            Assert.Equal("John Updated", returnValue.FirstName);
        }

        [Fact]
        public async Task Update_ReturnsNotFoundResult_WhenPatientDoesNotExist()
        {
            // Arrange
            _mockPatientManager.Setup(m => m.UpdatePatientAsync(99, It.IsAny<UpdatePatientRequestDto>())).ReturnsAsync((PatientDto)null);

            // Act
            var result = await _controller.Update(99, new UpdatePatientRequestDto { FirstName = "Nonexistent"});

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test Delete method
        [Fact]
        public async Task Delete_ReturnsOkResult_WithDeletedPatient()
        {
            // Arrange
            var deletedPatient = new PatientDto { PatientId = 1, FirstName = "John Doe", Age = 30 };
            _mockPatientManager.Setup(m => m.DeletePatientAsync(1)).ReturnsAsync(deletedPatient);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(1, returnValue.PatientId);
            Assert.Equal("John Doe", returnValue.FirstName);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenPatientDoesNotExist()
        {
            // Arrange
            _mockPatientManager.Setup(m => m.DeletePatientAsync(99)).ReturnsAsync((PatientDto)null);

            // Act
            var result = await _controller.Delete(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
