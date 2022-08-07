using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AcademicManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab3.Pages
{
    public class RegistrationModel : PageModel
    {
        public class CourseSelection
        {
            public Course TheCourse { get; set; }
            public bool Selected { get; set; }
        }

        [BindProperty]
        public string SelectedStudentId { get; set; }

        [BindProperty]
        public List<SelectListItem> StudentDropdownOptions { get; set; }

        [BindProperty]
        public List<AcademicRecord> AcademicRecordsOfSelectedStudent { get; set; }

        [BindProperty]
        public List<CourseSelection> CourseSelections { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public void OnGet(string orderby)
        {
            if (orderby != null)
            {
                HttpContext.Session.SetString("orderby", orderby);

                SelectedStudentId = HttpContext.Session.GetString("SelectedStudentCode");
                if (SelectedStudentId != null && SelectedStudentId != "-1")
                {
                    AcademicRecordsOfSelectedStudent = DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId);
                    if (AcademicRecordsOfSelectedStudent.Count() > 0)
                    {
                        SortAcademicRecordsOfSelectedStudent();
                    }
                    else
                    {
                        BuildCourseSelections();
                    }
                }
            }
            StudentDropdownOptions = BuildStudentDropdownOptions();
        }

        public void OnPostStudentSelected()
        {
            if (SelectedStudentId != "-1")
            {
                HttpContext.Session.SetString("SelectedStudentCode", SelectedStudentId);

                AcademicRecordsOfSelectedStudent = DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId);
                if (AcademicRecordsOfSelectedStudent.Count() > 0)
                {
                    SortAcademicRecordsOfSelectedStudent();
                    Message = "The student has registered for the following courses:";
                }
                else
                {
                    Message = "The student has not registered any course. Select course(s) to register.";
                    BuildCourseSelections();
                }
            }
            else
            {
                Message = "You must select a student!";
                AcademicRecordsOfSelectedStudent = null;
                CourseSelections = null;
                HttpContext.Session.Remove("SelectedStudentCode");
            }

            StudentDropdownOptions = BuildStudentDropdownOptions();
        }

        public void OnPostRegister()
        {
            foreach (CourseSelection cs in CourseSelections)
            {
                if (cs.Selected)
                {
                    AcademicRecord academicRecord = new AcademicRecord(SelectedStudentId, cs.TheCourse.CourseCode);
                    DataAccess.AddAcademicRecord(academicRecord);
                    AcademicRecordsOfSelectedStudent.Add(academicRecord);
                }
                SortAcademicRecordsOfSelectedStudent();
                Message = "The student has registered for the following courses. You can enter or edit the grades";
            }
            if (AcademicRecordsOfSelectedStudent.Count == 0)
            {
                Message = "You must select at least one course!";

                BuildCourseSelections();
            }

            StudentDropdownOptions = BuildStudentDropdownOptions();
        }

        public void OnPostGrade()
        {
            foreach (AcademicRecord ar in AcademicRecordsOfSelectedStudent)
            {
                DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId).First(a => a.CourseCode == ar.CourseCode).Grade = ar.Grade;
            }

            StudentDropdownOptions = BuildStudentDropdownOptions();
        }

        #region Helpers
        private List<SelectListItem> BuildStudentDropdownOptions()
        {
            List<SelectListItem> studentDropdownOptions = new List<SelectListItem>();
            foreach (Student s in AcademicManagement.DataAccess.GetAllStudents())
            {
                studentDropdownOptions.Add(new SelectListItem(s.StudentName, s.StudentId));
            }
            return studentDropdownOptions;
        }

        private void BuildCourseSelections()
        {
            CourseSelections = new List<CourseSelection>();
            foreach (Course c in AcademicManagement.DataAccess.GetAllCourses())
            {
                CourseSelections.Add(new CourseSelection() { TheCourse = c, Selected = false });
            }
        }

        private void SortAcademicRecordsOfSelectedStudent()
        {
            string orderby = HttpContext.Session.GetString("orderby");
            if (orderby == "code")
            {
                AcademicRecordsOfSelectedStudent.Sort((a, b) => a.CourseCode.CompareTo(b.CourseCode));
            }
            else if (orderby == "title")
            {
                AcademicRecordsOfSelectedStudent.Sort(new AcademicRecordComparerByCourseTitle());
            }
            else if (orderby == "grade")
            {
                AcademicRecordsOfSelectedStudent.Sort((a, b) => a.Grade.CompareTo(b.Grade));
            }

            #endregion
        }

        public class AcademicRecordComparerByCourseTitle : IComparer<AcademicRecord>
        {
            public int Compare(AcademicRecord x, AcademicRecord y)
            {
                Course xCourse = DataAccess.GetAllCourses().First(c => c.CourseCode == x.CourseCode);
                Course yCourse = DataAccess.GetAllCourses().First(c => c.CourseCode == y.CourseCode);

                return xCourse.CourseTitle.CompareTo(yCourse.CourseTitle);
            }
        }
    }
    
}
