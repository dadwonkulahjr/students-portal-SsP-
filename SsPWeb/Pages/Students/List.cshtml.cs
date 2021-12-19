using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SsPWeb.Data;
using SsPWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SsPWeb.Pages.Students
{
    public class ListModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Student> Students { get; set; }

        public ListModel(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task OnGet()
        {
            Students = await _context.Students
                           .Include(x => x.StudentDetails)
                           .ToListAsync();
                           

        }
    }
}
