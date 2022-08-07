using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicManagement;
namespace Lab2.Models
{
	public class Students
	{
        public string StuName { get; set; }
        public string StuId { get; set; }
        private static List<Students> studentList { get; set; }
        public static List<Students> StudentList
        {
            get
            {
                if (studentList == null)
                {
                    studentList = new List<Students>();
                    foreach (var item in DataAccess.GetAllStudents())
                    {
                        studentList.Add(new Students { StuId = item.StudentId, StuName = item.StudentName });
                    }
                }
                return studentList;
            }
        }


    }
    public class StudentSelection
    {
        public bool Selected { get; set; } = false;
        public Students TheStudent { get; set; }

        public StudentSelection()
        {
        }
        public StudentSelection(Students stu)
        {
            TheStudent = stu;
        }

    }

}
