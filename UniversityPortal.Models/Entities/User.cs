﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityPortal.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; } // 2 Faculty or  3 Student or 1 Admin

        public string RoleName =>
            RoleId switch
            {
                1 => "Admin",
                2 => "Faculty",
                3 => "Student",
                _ => "Unknown"
            };

    }

}
