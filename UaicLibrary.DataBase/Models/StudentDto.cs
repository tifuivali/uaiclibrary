using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UaicLibrary.DataBase.Models
{
    public class StudentDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        [StringLength(30)]
        public virtual string MatricolNumber { get; set; }
        [StringLength(255)]
        public virtual string Email { get; set; }
        [StringLength(255)]
        public virtual string FirstName { get; set; }
        [StringLength(255)]
        public virtual string LastName { get; set; }
        [StringLength(255)]
        public virtual string ImageUrl { get; set; }
        [StringLength(1024)]
        public virtual string About { get; set; }
        [StringLength(50)]
        public virtual string Password { get; set; }
        public virtual int? Year { get; set; }
        public virtual ICollection<FacultyStudent> FacultyStudents { get; set; }
        public virtual int ReadedBooks { get; set; }
        public virtual int OpennedBooks { get; set; }
        public virtual int EditedBooks { get; set; }
    }
}
