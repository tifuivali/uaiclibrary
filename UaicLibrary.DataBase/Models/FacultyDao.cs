using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UaicLibrary.DataBase.Models
{
    public class FacultyDao
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        [StringLength(255)]
        [Required]
        public virtual string Name { get; set; }
        [StringLength(255)]
        public virtual string Address { get; set; }
        public virtual ICollection<FacultyStudent>FacultyStudents { get; set; }
        public virtual ICollection<FacultyProfessor> FacultyProfessors { get; set; }
        public virtual ICollection<BookDto> Books { get; set; }
    }
}
