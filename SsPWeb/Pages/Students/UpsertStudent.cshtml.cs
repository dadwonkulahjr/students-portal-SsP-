using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                StudentViewModel.Student = new();
                return Page();
            }

            if (id.HasValue)
            {

                StudentViewModel.Student = _context.Students
                      .Include(x => x.StudentDetails)
                      .FirstOrDefault(x => x.Id == id);

                return Page();
            }


            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) { return Page(); }



            if (StudentViewModel.Student.Id == 0)
            {

                Student student = new()
                {
                    FirstName = StudentViewModel.Student.FirstName
                };
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                int id = student.Id;

                StudentDetails record = new()
                {
                    StudentDetailsId = id,
                    Address = StudentViewModel.Student.StudentDetails.Address,
                    Dob = StudentViewModel.Student.StudentDetails.Dob,
                    OrangeNumber = StudentViewModel.Student.StudentDetails.OrangeNumber,
                    LonestarNumber = StudentViewModel.Student.StudentDetails.LonestarNumber,
                    WhatAppNumber = StudentViewModel.Student.StudentDetails.WhatAppNumber
                };

                await _context.StudentDetails.AddAsync(record);
                await _context.SaveChangesAsync();
                TempData["success"] = "Student add successfully!";
                return RedirectToPage("list");
            }
            else
            {
                var student = await _context.Students
                      .Include(x => x.StudentDetails)
                        .FirstOrDefaultAsync(x => x.Id == StudentViewModel.Student.Id);

                if (student is null)
                {
                    return NotFound();
                }

                student.FirstName = StudentViewModel.Student.FirstName;
                int studentId = student.Id;
                _context.Students.Update(student);
                await _context.SaveChangesAsync();

                StudentDetails record = new();
                if (student.StudentDetails == null)
                {
                    record.StudentDetailsId = studentId;
                    record.Address = StudentViewModel.Student.StudentDetails.Address;
                    record.Dob = StudentViewModel.Student.StudentDetails.Dob;
                    record.LonestarNumber = StudentViewModel.Student.StudentDetails.LonestarNumber;
                    record.OrangeNumber = StudentViewModel.Student.StudentDetails.OrangeNumber;
                    record.WhatAppNumber = StudentViewModel.Student.StudentDetails.WhatAppNumber;
                    _context.StudentDetails.Add(record);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    record = await _context.StudentDetails.FirstOrDefaultAsync(x => x.StudentDetailsId == studentId);
                    record.StudentDetailsId = studentId;
                    record.Address = StudentViewModel.Student.StudentDetails.Address;
                    record.Dob = StudentViewModel.Student.StudentDetails.Dob;
                    record.LonestarNumber = StudentViewModel.Student.StudentDetails.LonestarNumber;
                    record.OrangeNumber = StudentViewModel.Student.StudentDetails.OrangeNumber;
                    record.WhatAppNumber = StudentViewModel.Student.StudentDetails.WhatAppNumber;

                    _context.StudentDetails.Update(record);
                    await _context.SaveChangesAsync();

                }
                TempData["success"] = "Student updated successfully!";
                return RedirectToPage("list");

            }

        }
    }
}
