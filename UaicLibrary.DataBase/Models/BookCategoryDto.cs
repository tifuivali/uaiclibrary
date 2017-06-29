using System.ComponentModel.DataAnnotations.Schema;

namespace UaicLibrary.DataBase.Models
{
    public class BookCategoryDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}
