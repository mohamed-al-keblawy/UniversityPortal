using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityPortal.BLL.Services;
using UniversityPortal.DAL.Interfaces;
using UniversityPortal.Models.Entities;

namespace UniversityPortal.Tests
{
    public class AssignmentServiceTests
    {
        [Fact]
        public async Task CreateAssignment_ShouldReturnValidId()
        {
            // Arrange
            var mockRepo = new Mock<IAssignmentRepository>();
            var service = new AssignmentService(mockRepo.Object);
            var assignment = new Assignment { Title = "Test", Description = "Test", DueDate = DateTime.Now, CreatedBy = 1 };

            mockRepo.Setup(r => r.CreateAsync(assignment)).ReturnsAsync(1);

            // Act
            var result = await service.CreateAssignmentAsync(assignment);

            // Assert
            Assert.Equal(1, result);
        }
    }

}
