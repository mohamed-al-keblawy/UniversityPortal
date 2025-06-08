using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityPortal.Models.Entities;

namespace UniversityPortal.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);

        Task<int> RegisterAsync(User user);


    }
}
