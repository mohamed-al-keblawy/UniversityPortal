using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using UniversityPortal.DAL.Interfaces;
using UniversityPortal.Models;
using UniversityPortal.Models.Entities;

public class SubmissionRepository(IConfiguration config) : ISubmissionRepository
{
    private readonly string _connectionString = config.GetConnectionString("DefaultConnection");

    public async Task<int> SubmitAsync(Submission submission)
    {
        using var connection = new SqlConnection(_connectionString);
        var parameters = new DynamicParameters();
        parameters.Add("@AssignmentId", submission.AssignmentId);
        parameters.Add("@SubmittedBy", submission.SubmittedBy);
        parameters.Add("@SubmissionUrl", submission.SubmissionUrl);
        parameters.Add("@SubmissionDate", submission.SubmissionDate);

        return await connection.ExecuteAsync("sp_SubmitAssignment", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Submission>> GetByStudentIdAsync(int studentId)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Submission>(
            "sp_GetSubmissionsByStudent",
            new { StudentId = studentId },
            commandType: CommandType.StoredProcedure
        );
    }
}
