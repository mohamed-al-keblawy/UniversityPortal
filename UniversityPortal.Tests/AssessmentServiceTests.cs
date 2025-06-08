using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityPortal.BLL.Services;
using UniversityPortal.DAL;
using UniversityPortal.DAL.Interfaces;
using UniversityPortal.Models;
using UniversityPortal.Models.Entities;
using Xunit;

public class AssessmentServiceTests
{
    [Fact]
    public async Task CreateAssessmentAsync_CallsRepo()
    {
        var assessment = new Assessment
        {
            AssignmentId = 1,
            StudentId = 3,
            AssessedBy = 2,
            Criteria = new List<AssessmentCriterion>
            {
                new AssessmentCriterion { CriterionName = "Clarity", Score = 9 }
            }
        };

        var mockRepo = new Mock<IAssessmentRepository>();
        mockRepo.Setup(r => r.CreateAssessmentAsync(assessment)).ReturnsAsync(10);
        var service = new AssessmentService(mockRepo.Object);

        var result = await service.CreateAssessmentAsync(assessment);

        Assert.Equal(10, result);
    }

    [Fact]
    public async Task GetByStudentIdAsync_ReturnsAssessments()
    {
        var assessments = new List<Assessment> { new Assessment { StudentId = 3 } };
        var mockRepo = new Mock<IAssessmentRepository>();
        mockRepo.Setup(r => r.GetByStudentIdAsync(3)).ReturnsAsync(assessments);
        var service = new AssessmentService(mockRepo.Object);

        var result = await service.GetByStudentIdAsync(3);

        Assert.Single(result);
    }
}
