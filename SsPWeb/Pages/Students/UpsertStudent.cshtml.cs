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
                      .Include(x => x.Record)
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
                    Name = StudentViewModel.Student.Name
                };
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                int id = student.Id;

                Record record = new()
                {
                    RecordId = id,
                    Address = StudentViewModel.Student.Record.Address,
                    Dob = StudentViewModel.Student.Record.Dob,
                    OrangeNumber = StudentViewModel.Student.Record.OrangeNumber,
                    LonestarNumber = StudentViewModel.Student.Record.LonestarNumber,
                    WhatAppNumber = StudentViewModel.Student.Record.WhatAppNumber
                };

                await _context.Records.AddAsync(record);
                await _context.SaveChangesAsync();
                TempData["success"] = "Student add successfully!";
                return RedirectToPage("list");
            }
            else
            {
                var student = await _context.Students
                      .Include(x => x.Record)
                        .FirstOrDefaultAsync(x => x.Id == StudentViewModel.Student.Id);

                if (student is null)
                {
                    return NotFound();
                }

                student.Name = StudentViewModel.Student.Name;
                int studentId = student.Id;
                _context.Students.Update(student);
                await _context.SaveChangesAsync();

                Record record = new();
                if (student.Record == null)
                {
                    record.RecordId = studentId;
                    record.Address = StudentViewModel.Student.Record.Address;
                    record.Dob = StudentViewModel.Student.Record.Dob;
                    record.LonestarNumber = StudentViewModel.Student.Record.LonestarNumber;
                    record.OrangeNumber = StudentViewModel.Student.Record.OrangeNumber;
                    record.WhatAppNumber = StudentViewModel.Student.Record.WhatAppNumber;
                    _context.Records.Add(record);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    record = await _context.Records.FirstOrDefaultAsync(x => x.RecordId == studentId);
                    record.RecordId = studentId;
                    record.Address = StudentViewModel.Student.Record.Address;
                    record.Dob = StudentViewModel.Student.Record.Dob;
                    record.LonestarNumber = StudentViewModel.Student.Record.LonestarNumber;
                    record.OrangeNumber = StudentViewModel.Student.Record.OrangeNumber;
                    record.WhatAppNumber = StudentViewModel.Student.Record.WhatAppNumber;

                    _context.Records.Update(record);
                    await _context.SaveChangesAsync();

                }
                TempData["success"] = "Student updated successfully!";
                return RedirectToPage("list");

            }

        }
    }
}
