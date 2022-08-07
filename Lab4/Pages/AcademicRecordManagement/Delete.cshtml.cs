using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab4.DataAccess;

namespace Lab4.Pages.AcademicRecordManagement
{
    public class DeleteModel : PageModel
    {
        private readonly Lab4.DataAccess.StudentRecordContext _context;

        public DeleteModel(Lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AcademicRecord AcademicRecord { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AcademicRecord = await _context.AcademicRecords.FindAsync(id);

            if (AcademicRecord != null)
            {
                _context.AcademicRecords.Remove(AcademicRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
