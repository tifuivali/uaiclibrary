using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UaicLibrary.DataBase.Models
{
    public class BookReadDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public virtual UserDto User { get; set; }
        public virtual BookDto Book { get; set; }
        public virtual DateTime Date { get; set; }
    }
}
