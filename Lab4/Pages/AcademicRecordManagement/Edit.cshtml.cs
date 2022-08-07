using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.DataAccess;

namespace Lab4.Pages.AcademicRecordManagement
{
    public class EditModel : PageModel
    {
        private readonly Lab4.DataAccess.StudentRecordContext _context;

        public EditModel(Lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AcademicRecord AcademicRecord { get; set; }
        public IList<Course> Course { get; set; }
        public IList<Student> Student { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AcademicRecord = await _context.AcademicRecords
                .Include(a => a.CourseCodeNavigation)
                .Include(a => a.Student).FirstOrDefaultAsync(m => m.StudentId == id);

            if (AcademicRecord == null)
            {
                return NotFound();
            }
           ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code");
           ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AcademicRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicRecordExists(AcademicRecord.StudentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AcademicRecordExists(string id)
        {
            return _context.AcademicRecords.Any(e => e.StudentId == id);
        }
    }
}
