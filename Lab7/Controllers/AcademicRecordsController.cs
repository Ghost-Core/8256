using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab7.Models.DataAccess;
using Lab7.Models;

namespace Lab7.Controllers
{
    public class AcademicRecordsController : Controller
    {
        private readonly StudentRecordContext _context;

        public AcademicRecordsController(StudentRecordContext context)
        {
            _context = context;
        }

        // GET: AcademicRecords
        public async Task<IActionResult> Index(string orderby)
        {
            var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student);
            var unsortedList = await studentRecordContext.ToListAsync();
            if (orderby == "course")
            {
                var sortedList = from c in unsortedList orderby c.CourseCodeNavigation.Title, c.Student.Name ascending select c;
                return View(sortedList);
            }
            if (orderby == "student")
            {
                var sortedList = from c in unsortedList orderby c.Student.Name, c.CourseCodeNavigation.Title ascending select c;
                return View(sortedList);
            }

            return View(unsortedList);
        }


        // GET: AcademicRecords/Create
        public IActionResult Create()
        {
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: AcademicRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseCode,StudentId,Grade")] AcademicRecord academicRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code", academicRecord.CourseCode);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", academicRecord.StudentId);
            return View(academicRecord);
        }

        // GET: AcademicRecords/Edit/5
        public async Task<IActionResult> Edit(string id, string code)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicRecords = await _context.AcademicRecords.ToListAsync();
            var student = await _context.Students.ToListAsync();
            var course = await _context.Courses.ToListAsync();

            AcademicRecord record = null;

            foreach (var item in academicRecords)
            {
                if (item.Student.Id == id && item.CourseCodeNavigation.Code == code)
                {
                    record = item;
                    break;
                }
            }
            if (record == null)
            {
                return NotFound();
            }
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View(record);
        }

        // POST: AcademicRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CourseCode,StudentId,Grade")] AcademicRecord academicRecord)
        {
            if (id != academicRecord.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicRecordExists(academicRecord.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code", academicRecord.CourseCode);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", academicRecord.StudentId);
            return View(academicRecord);
        }

        public async Task<IActionResult> EditAll(string orderby)
        {
            EditAllAcademicRecords editAllAcademicRecords = new EditAllAcademicRecords();
            foreach (var academicRecord in _context.AcademicRecords)
            {
                editAllAcademicRecords.AcademicRecords.Add(academicRecord);
            }

            return View(editAllAcademicRecords);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAll(EditAllAcademicRecords editAllAcademicRecords)
        {
            if (ModelState.IsValid)
            {
                foreach (AcademicRecord academicRecord in editAllAcademicRecords.AcademicRecords)
                {
                    _context.Update(academicRecord);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(editAllAcademicRecords);
        }

        private bool AcademicRecordExists(string id)
        {
            return _context.AcademicRecords.Any(e => e.StudentId == id);
        }
    }
}
