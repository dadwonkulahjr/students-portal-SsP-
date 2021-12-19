using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SsPWeb.Models
{
    public class Gender
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }

        public virtual IEnumerable<Student> Students { get; set; }
    }
}
