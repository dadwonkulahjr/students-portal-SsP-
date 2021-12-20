using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SsPWeb.Data;
using SsPWeb.Models;
using SsPWeb.ViewModels;

namespace SsPWeb.Pages.Students
{
    public class UpsertStudentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UpsertStudentModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public StudentViewModel StudentViewModel { get; set; } = new();
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                StudentViewModel.Student = new();
                StudentViewModel.Student.Gender = new();
                StudentViewModel.Genders = await _context.Genders.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToListAsync();

                return Page();
            }

            if (id.HasValue)
            {

                StudentViewModel.Student = _context.Students
                      .Include(x => x.Gender)
                      .Include(x => x.StudentDetails)
                      .FirstOrDefault(x => x.Id == id);

                StudentViewModel.Genders = await _context.Genders
                                    .Select(x => new SelectListItem()
                                    {
                                        Text = x.Name,
                                        Value = x.Id.ToString()
                                    }).ToListAsync();

                return Page();
            }


            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                StudentViewModel.Student = _context.Students
                      .Include(x => x.Gender)
                      .Include(x => x.StudentDetails)
                      .FirstOrDefault(x => x.Id == StudentViewModel.Student.Id);

                StudentViewModel.Genders = await _context.Genders
                                    .Select(x => new SelectListItem()
                                    {
                                        Text = x.Name,
                                        Value = x.Id.ToString()
                                    }).ToListAsync();

                return Page();
            }



            if (StudentViewModel.Student.Id == 0)
            {

                Student student = new()
                {
                    FirstName = StudentViewModel.Student.FirstName,
                    MiddleName = StudentViewModel.Student.MiddleName,
                    LastName = StudentViewModel.Student.LastName,
                    Email = StudentViewModel.Student.Email,
                    GenderId = StudentViewModel.Student.GenderId
                };

                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                int id = student.Id;

                StudentDetails studentDetails = new()
                {
                    StudentDetailsId = id,
                    Address = StudentViewModel.Student.StudentDetails.Address,
                    Dob = StudentViewModel.Student.StudentDetails.Dob,
                    OrangeNumber = StudentViewModel.Student.StudentDetails.OrangeNumber,
                    LonestarNumber = StudentViewModel.Student.StudentDetails.LonestarNumber,
                    WhatAppNumber = StudentViewModel.Student.StudentDetails.WhatAppNumber,
                    StudentIdNumber = StudentViewModel.Student.StudentDetails.StudentIdNumber
                };

                await _context.StudentDetails.AddAsync(studentDetails);
                await _context.SaveChangesAsync();
                TempData["success"] = "Student add successfully!";
                return RedirectToPage("list");
            }
            else
            {
                var student = await _context.Students
                      .Include(x => x.StudentDetails)
                      .Include(x => x.Gender)
                        .FirstOrDefaultAsync(x => x.Id == StudentViewModel.Student.Id);

                if (student is null)
                {
                    return NotFound();
                }

                student.FirstName = StudentViewModel.Student.FirstName;
                student.MiddleName = StudentViewModel.Student.MiddleName;
                student.LastName = StudentViewModel.Student.LastName;
                student.Email = StudentViewModel.Student.Email;
                student.GenderId = StudentViewModel.Student.GenderId;
                int studentId = student.Id;
                _context.Students.Update(student);
                await _context.SaveChangesAsync();

                StudentDetails studentDetails = new();
                if (student.StudentDetails == null)
                {
                    studentDetails.StudentDetailsId = studentId;
                    studentDetails.Address = StudentViewModel.Student.StudentDetails.Address;
                    studentDetails.Dob = StudentViewModel.Student.StudentDetails.Dob;
                    studentDetails.LonestarNumber = StudentViewModel.Student.StudentDetails.LonestarNumber;
                    studentDetails.OrangeNumber = StudentViewModel.Student.StudentDetails.OrangeNumber;
                    studentDetails.WhatAppNumber = StudentViewModel.Student.StudentDetails.WhatAppNumber;
                    studentDetails.Student.StudentDetails.StudentIdNumber = StudentViewModel.Student.StudentDetails.StudentIdNumber;
                    student.GenderId = StudentViewModel.Student.GenderId;
                    _context.StudentDetails.Add(studentDetails);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    studentDetails = await _context.StudentDetails.FirstOrDefaultAsync(x => x.StudentDetailsId == studentId);
                    studentDetails.StudentDetailsId = studentId;
                    studentDetails.Address = StudentViewModel.Student.StudentDetails.Address;
                    studentDetails.Dob = StudentViewModel.Student.StudentDetails.Dob;
                    studentDetails.LonestarNumber = StudentViewModel.Student.StudentDetails.LonestarNumber;
                    studentDetails.OrangeNumber = StudentViewModel.Student.StudentDetails.OrangeNumber;
                    studentDetails.WhatAppNumber = StudentViewModel.Student.StudentDetails.WhatAppNumber;
                    studentDetails.Student.StudentDetails.StudentIdNumber = StudentViewModel.Student.StudentDetails.StudentIdNumber;
                    student.GenderId = StudentViewModel.Student.GenderId;
                    _context.StudentDetails.Update(studentDetails);
                    await _context.SaveChangesAsync();

                }
                TempData["success"] = "Student updated successfully!";
                return RedirectToPage("list");

            }

        }
    }
}
