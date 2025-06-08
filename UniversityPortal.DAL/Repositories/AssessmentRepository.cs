using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using UniversityPortal.DAL.Interfaces;
using UniversityPortal.Models;
using UniversityPortal.Models.Entities;

public class AssessmentRepository(IConfiguration configuration) : IAssessmentRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

    public async Task<int> CreateAssessmentAsync(Assessment assessment)
    {
        using var connection = new SqlConnection(_connectionString);

        var outputParams = new DynamicParameters();
        outputParams.Add("@AssignmentId", assessment.AssignmentId);
        outputParams.Add("@StudentId", assessment.StudentId);
        outputParams.Add("@AssessedBy", assessment.AssessedBy);
        outputParams.Add("@AssessmentId", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await connection.ExecuteAsync("sp_CreateAssessment", outputParams, commandType: CommandType.StoredProcedure);
        int assessmentId = outputParams.Get<int>("@AssessmentId");

        foreach (var criterion in assessment.Criteria)
        {
            var critParams = new DynamicParameters();
            critParams.Add("@AssessmentId", assessmentId);
            critParams.Add("@CriterionName", criterion.CriterionName);
            critParams.Add("@Score", criterion.Score);
            critParams.Add("@Remarks", criterion.Remarks);

            await connection.ExecuteAsync("sp_AddAssessmentCriterion", critParams, commandType: CommandType.StoredProcedure);
        }

        return assessmentId;
    }

    public async Task<IEnumerable<Assessment>> GetByStudentIdAsync(int studentId)
    {
        using var connection = new SqlConnection(_connectionString);

        var assessments = await connection.QueryAsync<Assessment>(
            "sp_GetAssessmentByStudent",
            new { StudentId = studentId },
            commandType: CommandType.StoredProcedure
        );

        foreach (var assessment in assessments)
        {
            var criteria = await connection.QueryAsync<AssessmentCriterion>(
                "sp_GetCriteriaByAssessment",
                new { AssessmentId = assessment.AssessmentId },
                commandType: CommandType.StoredProcedure
            );

            assessment.Criteria = criteria.ToList();
        }

        return assessments;
    }
}
