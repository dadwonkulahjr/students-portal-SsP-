using System.ComponentModel.DataAnnotations;

namespace SsPWeb.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }

        public virtual Record Record { get; set; }

    }
}
