using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SsPWeb.Models
{
    public class Record
    {
        [ForeignKey(nameof(Student))]
        public int RecordId { get; set; }
        [Required, StringLength(50)]
        public string Address { get; set; }
        [StringLength(25)]
        public string LonestarNumber { get; set; }
        [StringLength(25)]
        public string OrangeNumber { get; set; }
        [Required, StringLength(25)]
        public string WhatAppNumber { get; set; }
        [Column(TypeName ="date"), Required]
        public DateTime? Dob { get; set; }

        public virtual Student Student { get; set; }
    }
}
