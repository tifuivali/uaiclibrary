namespace UaicLibrary.DataBase.Models
{
    public class FacultyProfessor
    {
        public virtual int ProfessorId { get; set; }
        public virtual ProfessorDto Professor { get; set; }
        public virtual int FacultyId { get; set; }
        public virtual FacultyDao Faculty { get; set; }
    }
}
