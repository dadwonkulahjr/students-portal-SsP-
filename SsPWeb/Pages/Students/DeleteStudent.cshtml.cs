using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SsPWeb.Data;

namespace SsPWeb.Pages.Students
{
    public class DeleteStudentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteStudentModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostDeleteStudent(int id)
        {
            var studentToRemove = await _context.Students
                                            .Include(x => x.StudentDetails)
                                            .FirstOrDefaultAsync(x => x.Id == id);

            if (studentToRemove is null) { return NotFound(); }


            var result = _context.Students.Remove(studentToRemove);
            result.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            TempData["success"] = "Student Deleted successfully!";
            return RedirectToPage("list");
        }
    }
}
