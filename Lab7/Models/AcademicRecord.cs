using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab7.Models.DataAccess
{
    public partial class AcademicRecord
    {

        [NotMapped]
        public string CourseDisplayTest
        {
            get
            {
                using StudentRecordContext _context = new StudentRecordContext();
                Course course = _context.Courses.Find(this.CourseCode);
                return course.Code + " - " + course.Title;
            }
        }

        [NotMapped]
        public string StudentDisplayTest
        {
            get
            {
                using StudentRecordContext _context = new StudentRecordContext();
                Student student = _context.Students.Find(this.StudentId);
                return student.Id + " - " + student.Name;
            }
        }

    }
}
