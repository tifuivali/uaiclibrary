using System.ComponentModel.DataAnnotations;

namespace UaicLibrary.DataBase.Models
{
    public class MatricolNumberDto
    {
        [Key]
        public virtual string Matricol { get; set; }
    }
}
