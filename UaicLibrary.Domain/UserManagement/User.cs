using System.Collections.Generic;
using UaicLibrary.Domain.FacultyManagement;

namespace UaicLibrary.Domain.UserManagement
{
    public class User
    {
        public int Id { get; set; }
        public string MatricolNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Role { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public IList<Faculty> Faculties { get; set; } 
    }
}
