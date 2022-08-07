using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Lab7.Models.EntityMetadata
{
    public class AcademicRecordMetadata
    {
        [Range(0, 100, ErrorMessage = "Must be between 0 and 100")]
        public int? Grade { get; set; }
    }
}
