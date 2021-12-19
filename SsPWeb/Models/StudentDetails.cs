using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SsPWeb.Models
{
    public class StudentDetails
    {
        [ForeignKey(nameof(Student))]
        public int StudentDetailsId { get; set; }
        [Required, StringLength(50)]
        public string Address { get; set; }
        [StringLength(25), Display(Name ="Lonestar Contact")]
        public string LonestarNumber { get; set; }
        [StringLength(25), Display(Name = "Orange Contact")]
        public string OrangeNumber { get; set; }
        [Required, StringLength(25), Display(Name = "WhatApp Contact")]
        public string WhatAppNumber { get; set; }
        [Column(TypeName ="date"), Required]
        public DateTime? Dob { get; set; }
        [Required]
        public int StudentIdNumber { get; set; }

        public virtual Student Student { get; set; }
    }
}
