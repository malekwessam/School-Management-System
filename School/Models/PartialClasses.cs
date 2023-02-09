using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School.Models
{
    [MetadataType(typeof(StudentMetaData))]
    public partial class Student
    {
        public String Fullname
        {
            get
            {
                return fName + " " + LName;
            }
        }

    }
    [MetadataType(typeof(CoursesMetaData))]
    public partial class Course
    {
    }
    [MetadataType(typeof(EnrollmentMetaData))]
    public partial class Enrollment
    {
    }


}