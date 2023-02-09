using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School.Models;

namespace School.Controllers
{
    public class BaseController : Controller
    {
        private SchoolEntities db = new SchoolEntities();
        static List<CourseStats> Stats = null;
        static List<StudentStats> StudentStats = null;
        public BaseController()
        {
            Stats = new List<CourseStats>();
            

            var CourseGroup = db.Enrollments.GroupBy(g => g.Course.courseName);
            foreach (var group in CourseGroup)
            {
                Stats.Add(new CourseStats { CourseTitle = group.Key, CourseEnrollments = group.Count() });
            }
            ViewBag.CourseStats = Stats;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            StudentStats = new List<StudentStats>();
            var stdsTop = db.Enrollments.GroupBy(g => g.StudentID).Select(b=> new StudentStats
            {
                StudentID=b.Key,
                FullName=b.FirstOrDefault().Student.fName + " " + b.FirstOrDefault().Student.LName,
                NomberOfCourses=b.Count(),
                AverageGrade=(decimal)b.Average(s=> s.Grade),
                Image=b.FirstOrDefault().Student.imageBath,
            }).OrderByDescending(x => x.AverageGrade).Take(3).ToList();

            ViewBag.stdsTop = stdsTop;




        }



    }
}