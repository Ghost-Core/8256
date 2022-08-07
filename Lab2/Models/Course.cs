using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicManagement; 

namespace Lab2.Models
{
    public class Courses
    {
        public string Code { get; set; }
        public string Title { get; set; }


        private static List<Courses> availableCourses;
        public static List<Courses> AvailableCourses
        {
            get
            {
                if (availableCourses == null)
                {
                    availableCourses = new List<Courses>();
                    foreach (var item in DataAccess.GetAllCourses())
                    {
                        availableCourses.Add(new Courses { Code = item.CourseCode, Title = item.CourseTitle });
                    }
                
                }
                return availableCourses;
            }
        }
    }
    public class CourseSelection
    {
        public bool Selected { get; set; } = false;
        public Courses TheCourse { get; set; }

        public CourseSelection()
        {
        }
        public CourseSelection(Courses cs)
        {
            TheCourse = cs;
        }

    }

}

