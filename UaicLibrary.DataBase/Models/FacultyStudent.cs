namespace UaicLibrary.DataBase.Models
{
    public class FacultyStudent
    {
        public virtual int StudentId { get; set; }
        public virtual StudentDto Student { get; set; }
        public virtual int FacultyId { get; set; }
        public virtual FacultyDao Faculty { get; set; }
    }
}
