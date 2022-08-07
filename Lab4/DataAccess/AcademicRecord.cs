using System;
using System.Collections.Generic;

#nullable disable

namespace Lab4.DataAccess
{
    public partial class AcademicRecord
    {
        public string CourseCode { get; set; }
        public string StudentId { get; set; }
        public int? Grade { get; set; }

        public virtual Course CourseCodeNavigation { get; set; }
        public virtual Student Student { get; set; }
        public string DisplayText { get { return CourseCode + " - " + CourseCodeNavigation.Title; } }
    }
    
}
