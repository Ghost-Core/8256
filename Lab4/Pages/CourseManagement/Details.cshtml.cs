using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab4.DataAccess;

namespace Lab4.Pages.CourseManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Lab4.DataAccess.StudentRecordContext _context;

        public DetailsModel(Lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Courses.FirstOrDefaultAsync(m => m.Code == id);

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
