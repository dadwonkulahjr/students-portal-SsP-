using Microsoft.AspNetCore.Mvc.Rendering;
using SsPWeb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SsPWeb.ViewModels
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
      
        public IEnumerable<SelectListItem> Genders { get; set; }
        //public string Address { get; set; }
        //public string LonestarNumber { get; set; }

        //public string OrangeNumber { get; set; }

        //public string WhatAppNumber { get; set; }

        //public DateTime? DateOfBirth { get; set; }
    }
}
