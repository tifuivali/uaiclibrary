using System.Collections.Generic;
using UaicLibrary.Domain.FacultyManagement;

namespace UaicLibrary.Domain.StudentManagement
{
    public class Student
    {
        public int Id { get; set; }
        public string MatricolNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int ReadedBooks { get; set; }
        public int OpennedBooks { get; set; }
        public string ImageUrl { get; set; }
        public int EditedBooks { get; set; }
        public int? Year { get; set; }
        public IList<Faculty> Faculties { get; set; }
    }
}
