using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityPortal.BLL.Services;
using UniversityPortal.DAL;
using UniversityPortal.DAL.Interfaces;
using UniversityPortal.Models;
using UniversityPortal.Models.Entities;
using Xunit;

public class SubmissionServiceTests
{
    [Fact]
    public async Task SubmitAsync_CallsRepo()
    {
        var submission = new Submission { AssignmentId = 1, SubmittedBy = 3 };
        var mockRepo = new Mock<ISubmissionRepository>();
        mockRepo.Setup(r => r.SubmitAsync(submission)).ReturnsAsync(1);
        var service = new SubmissionService(mockRepo.Object);

        var result = await service.SubmitAsync(submission);

        Assert.Equal(1, result);
        mockRepo.Verify(r => r.SubmitAsync(submission), Times.Once);
    }

    [Fact]
    public async Task GetByStudentAsync_ReturnsSubmissions()
    {
        var list = new List<Submission> { new Submission { AssignmentId = 1 } };
        var mockRepo = new Mock<ISubmissionRepository>();
        mockRepo.Setup(r => r.GetByStudentIdAsync(3)).ReturnsAsync(list);
        var service = new SubmissionService(mockRepo.Object);

        var result = await service.GetByStudentAsync(3);

        Assert.Single(result);
    }
}
