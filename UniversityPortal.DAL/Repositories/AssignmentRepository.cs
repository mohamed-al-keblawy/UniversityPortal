using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityPortal.DAL.Interfaces;
using UniversityPortal.Models.Entities;

namespace UniversityPortal.DAL.Repositories
{
    public class AssignmentRepository(IConfiguration config) : IAssignmentRepository
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection");

        public async Task<IEnumerable<Assignment>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Assignment>("sp_GetAllAssignments", commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateAsync(Assignment assignment)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Title", assignment.Title);
            parameters.Add("@Description", assignment.Description);
            parameters.Add("@DueDate", assignment.DueDate);
            parameters.Add("@CreatedBy", assignment.CreatedBy);

            var result = await connection.ExecuteScalarAsync<int>(
                "sp_CreateAssignment",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<Assignment> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@AssignmentId", id);

            var result = await connection.QuerySingleOrDefaultAsync<Assignment>(
                "sp_GetAssignmentById",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }
        

}
