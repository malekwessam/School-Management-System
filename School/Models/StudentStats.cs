using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Models
{
    public class StudentStats
    {
        public int StudentID { get; set; }
        public String FullName { get; set; }
        public int NomberOfCourses { get; set; }
        public decimal AverageGrade { get; set; }

        public String Image { get; set; }


    }
}