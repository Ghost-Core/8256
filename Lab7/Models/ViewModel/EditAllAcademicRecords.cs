using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab7.Models.DataAccess;

namespace Lab7.Models
{
    public class EditAllAcademicRecords
    {

        public List<AcademicRecord> AcademicRecords { get; set; }

        public EditAllAcademicRecords()
        {
            AcademicRecords = new();
        }

        public EditAllAcademicRecords(List<AcademicRecord> AcademicRecords)
        {
            this.AcademicRecords = AcademicRecords;
        }

    }
}
