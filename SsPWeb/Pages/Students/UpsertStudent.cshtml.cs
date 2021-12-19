using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsPWeb.Models;

namespace SsPWeb.Pages.Students
{
    public class UpsertStudentModel : PageModel
    {
        [BindProperty]
        public Student Student { get; set; }
        public async Task OnGet(int? id)
        {
            if (id == null)
            {

            }

        }
    }
}
