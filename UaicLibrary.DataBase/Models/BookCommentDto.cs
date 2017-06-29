using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UaicLibrary.DataBase.Models
{
    public class BookCommentDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public virtual UserDto User { get; set; }
        [StringLength(4096)]
        public virtual string Text { get; set; }
        public virtual int Likes { get; set; }
        public virtual int Dislikes { get; set; }
        public virtual DateTime Date { get; set; }
    }
}
