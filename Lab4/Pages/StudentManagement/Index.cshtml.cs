using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab4.DataAccess;

namespace Lab4.Pages.StudentManagement
{
    public class IndexModel : PageModel
    {
        private readonly StudentRecordContext _context;

        public IndexModel(StudentRecordContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public IList<Student> Student { get;set; }
        public List<AcademicRecord> AcademicRecord { get; set; }


        public async Task<IActionResult> OnGetAsync(string? sortOrder, string? delete)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            IQueryable<Student> studentsIQ = from s in _context.Students
                                             select s;

            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.Name);
                    break;
                
            }
            if (delete != null)
            {
                Student studentToDelete = _context.Students.FirstOrDefault(c => c.Id == delete);
                if (studentToDelete != null)
                {
                    while (studentToDelete != null) {
                        AcademicRecord academicRecordToDelete = _context.AcademicRecords.FirstOrDefault(c => c.StudentId == delete);
                        _context.AcademicRecords.Remove(academicRecordToDelete); }
                    
                    _context.Students.Remove(studentToDelete);
                    await _context.SaveChangesAsync();

                }
            }
            AcademicRecord = await _context.AcademicRecords
                .Include(a => a.CourseCodeNavigation)
                .Include(a => a.Student).ToListAsync();
            Student = await _context.Students.ToListAsync();
            Student = await studentsIQ.AsNoTracking().ToListAsync();
            return Page();
        }

        

        
    }
}
