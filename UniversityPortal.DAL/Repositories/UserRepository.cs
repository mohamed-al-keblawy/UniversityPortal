using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityPortal.DAL.Interfaces;
using UniversityPortal.Models.Entities;

namespace UniversityPortal.DAL.Repositories
{
    public class UserRepository(IConfiguration config) : IUserRepository
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection");

        public async Task<User?> GetByUsernameAsync(string username)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new { Username = username };

            return await connection.QueryFirstOrDefaultAsync<User>(
                "sp_GetUserByUsername",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<User>(
                "sp_GetUserByEmail",
                new { Email = email },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> RegisterAsync(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Username", user.Username);
            parameters.Add("@Email", user.Email);
            parameters.Add("@PasswordHash", user.PasswordHash);
            parameters.Add("@RoleId", user.RoleId);

            return await connection.ExecuteAsync(
                "sp_RegisterUser",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }



    }
}
