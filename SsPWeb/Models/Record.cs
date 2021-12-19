using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SsPWeb.Models
{
    public class Record
    {
        public int StudentId { get; set; }
        public string Address { get; set; }

        public string LonestarNumber { get; set; }
        public string OrangeNumber { get; set; }
        public string WhatAppNumber { get; set; }
        [Column(TypeName ="date")]
        public DateTime? Dob { get; set; }

        public virtual Student Student { get; set; }
    }
}
