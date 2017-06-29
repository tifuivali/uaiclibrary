using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UaicLibrary.DataBase.Models
{
    public class SettingDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        [StringLength(255)]
        public virtual string Key { get; set; }
        [StringLength(200000)]
        public virtual string Value { get; set; }
    }   
}
