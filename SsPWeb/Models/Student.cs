using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SsPWeb.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required, StringLength(50), Display(Name ="First name")]
        public string FirstName { get; set; }
        [Display(Name ="Middle name")]
        public char? MiddleName { get; set; }
        [StringLength(50), Display(Name ="Last name"), Required]
        public string LastName { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }


        //Foreign Key
        [ForeignKey(nameof(Gender)), Required(ErrorMessage ="Gender field is required.")]
        public int GenderId { get; set; }

        //Navigation Properties
        public virtual StudentDetails StudentDetails { get; set; }
        public virtual Gender Gender { get; set; }

    }
}
