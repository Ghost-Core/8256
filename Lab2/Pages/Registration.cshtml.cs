using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AcademicManagement;
using Lab2.Models;

namespace Lab2.Pages
{
    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public List<SelectListItem>StudentOptions { get; set; }
        [BindProperty]
        public List<CourseSelection> CourseSelections { get; set; }

        [BindProperty]
        public List<Models.Courses> SelectedCourses { get; set; }
        [BindProperty]
        public string SelectedStudentValue { get; set; }
        [BindProperty]
        public Students SelectedStudent { get; set; }
        public void OnGet()
        {
            CourseSelections = new List<CourseSelection>();
            foreach (Models.Courses cs in Courses.AvailableCourses)
            {
                CourseSelections.Add(new CourseSelection(cs));
            }

            StudentOptions = new List<SelectListItem>();
            foreach (Students stu in Students.StudentList)
            {
                StudentOptions.Add(new SelectListItem { Value = stu.StuId, Text = stu.StuName });

            }
        }
        public void OnPost()
        {
            SelectedCourses = new List<Courses>();
            foreach (CourseSelection cs in CourseSelections)
            {
                if (cs.Selected)
                {
                    SelectedCourses.Add(Courses.AvailableCourses.First(c => c.Code == cs.TheCourse.Code));
                }
            }

            if (SelectedStudentValue != "-1")
            {
                SelectedStudent = Students.StudentList.First(s => s.StuId == SelectedStudentValue);
                
            }
            else
            {
                SelectedStudent = null;
            }

            {
                StudentOptions = new List<SelectListItem>();
                foreach (Students stu in Students.StudentList)
                {
                    StudentOptions.Add(new SelectListItem { Value = stu.StuId, Text = stu.StuName });
                }
            }
        }
    }
}
    