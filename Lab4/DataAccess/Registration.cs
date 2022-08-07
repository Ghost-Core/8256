using System;
using System.Collections.Generic;

#nullable disable

namespace Lab4.DataAccess
{
    public partial class Registration
    {
        public string CourseCourseId { get; set; }
        public string StudentStudentNum { get; set; }

        public virtual Course CourseCourse { get; set; }
        public virtual Student StudentStudentNumNavigation { get; set; }
    }
}
