﻿using Moq;
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
        public async Task CreateAssignmentAsync_ShouldCallRepositoryAndReturnId()
        {
            // Arrange
            var mockRepo = new Mock<IAssignmentRepository>();
            var service = new AssignmentService(mockRepo.Object);
            var assignment = new Assignment { Title = "Test", CreatedBy = 2 };

            mockRepo.Setup(r => r.CreateAsync(assignment)).ReturnsAsync(1);

            // Act
            var result = await service.CreateAssignmentAsync(assignment);

            // Assert
            Assert.Equal(1, result);
            mockRepo.Verify(r => r.CreateAsync(assignment), Times.Once);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnsAssignment()
        {
            var assignment = new Assignment { AssignmentId = 5, Title = "Sample" };
            var mockRepo = new Mock<IAssignmentRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(assignment);
            var service = new AssignmentService(mockRepo.Object);

            var result = await service.GetByIdAsync(5);

            Assert.NotNull(result);
            Assert.Equal("Sample", result.Title);
        }
    }


}
