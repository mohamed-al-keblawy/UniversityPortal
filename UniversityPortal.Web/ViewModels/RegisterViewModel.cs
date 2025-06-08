namespace UniversityPortal.Web.ViewModels
{
    public class RegisterViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; } // 1=Admin, 2=Faculty, 3=Student
    }

}
