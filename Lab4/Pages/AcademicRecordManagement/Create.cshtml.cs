using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab4.DataAccess;

namespace Lab4.Pages.AcademicRecordManagement
{
    public class CreateModel : PageModel
    {
        private readonly Lab4.DataAccess.StudentRecordContext _context;

        public CreateModel(Lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code");
        ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return Page();
        }
        public string ErrorMessage{ get; set; }
            [BindProperty]
        public AcademicRecord AcademicRecord { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.AcademicRecords.Any(c => c.StudentId == AcademicRecord.StudentId))
            {
                if (_context.AcademicRecords.Any(c => c.CourseCode == AcademicRecord.CourseCode))
                {
                    if (_context.AcademicRecords.Any(c => c.Grade == AcademicRecord.Grade))
                    {
                        ErrorMessage = $"The specified academic record already exist!";
                        return Page();
                    }
                }
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AcademicRecords.Add(AcademicRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }


    }

}
