using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School.Models
{
    public class StudentMetaData
    {
    }

    public class CoursesMetaData
    {
        [Required(ErrorMessage ="Please Enter a Title!")]
        [Display(Name ="Title")]
        public string courseName { get; set; }
        [Required(ErrorMessage = "Please Enter the Course Credits!")]
        [Range(2, 6, ErrorMessage = "Please Enter the Course Credits!")]
        public int credits { get; set; }
        [Display(Name = "Description")]
        public string courseDescribtion { get; set; }

        [Required(ErrorMessage = "Please Enter the Course Rating!")]
        //[EnumDataType(typeof(CourseLevel), ErrorMessage = "Please Enter the Course level!")]
        public Nullable<CourseLevel> Level2 { get; set; }
        
        [Required(ErrorMessage = "Please Enter the Course Rating!")]
        [EnumDataType(typeof(RatingCourse), ErrorMessage = "Please Enter the Course Rating!")]
        
        public Nullable<RatingCourse> Rating { get; set; }
    }

    public class EnrollmentMetaData
    {
    }
}