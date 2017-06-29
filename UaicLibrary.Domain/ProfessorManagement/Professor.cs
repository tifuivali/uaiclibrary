using System.Collections.Generic;
using UaicLibrary.Domain.FacultyManagement;

namespace UaicLibrary.Domain.ProfessorManagement
{
    public class Professor
    {
        public int Id { get; set; }
        public string MatricolNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string ImageUrl { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public string Password { get; set; }
        public IList<Faculty> Faculties { get; set; }
    }
}
