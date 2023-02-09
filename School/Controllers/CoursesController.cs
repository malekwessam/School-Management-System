using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using School.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace School.Controllers
{
    public class CoursesController : BaseController
    {
        private SchoolEntities db = new SchoolEntities();

        // GET: Courses
        public async Task<ActionResult> Index(String searchString)
        {
            var courses = from c in db.Courses
                          select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(s => s.courseName.Contains(searchString));
            }
            TempData["ExcelDownload"] = "Excel File Downloaded!";
            return View(await courses.ToListAsync());
        }

        public JsonResult GetSearchValue(string search)
        {
            var suggestedCourses = db.Courses
                                    .Where(c => c.courseName.Contains(search))
                                    .Select(x => new
                                    {
                                        Title = x.courseName
                                    }).ToList();

            return new JsonResult { Data = suggestedCourses, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult StudentsInCourse( String CourseTitle, HttpPostedFileBase file)
        {
            //if (file != null)
            //{
            //    string pic = System.IO.Path.GetFileName(file.FileName);
            //    string path = System.IO.Path.Combine(Server.MapPath("~/Images/Profile/"), pic);
            //    file.SaveAs(path);
            //     = pic;
            //}
            var Stds = db.Enrollments.Where(c => c.Course.courseName == CourseTitle);
            return View(Stds);
        }

        // GET: Courses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.Level2 = new SelectList(db.CourseLevel1,"CourseLevelId","level");
            return View();
        }

        public ActionResult GetPartialNewCourse()
        {
            ViewBag.Level2 = new SelectList(db.CourseLevel1, "CourseLevelId", "Level");

            

            // ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Title");


            return PartialView("_PartialNewCourse");
        }
        [HttpGet]
        public ActionResult DeleteMultipleCourses()
        {
            return View(db.Courses.ToList());
        }
        [HttpPost]
        public ActionResult DeleteMultipleCourses(FormCollection formCollection)
        {
            //3,5,8,11
            try
            {
                string[] ids = formCollection["ID"].Split(new char[] { ',' });



                foreach (var id in ids)
                {
                    var course = db.Courses.Find(int.Parse(id));
                    db.Courses.Remove(course);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {


            }


            return View(db.Courses.ToList());
        }


        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "courseID,courseName,credits,Level2,courseDescribtion,price,Rating,IsCourseActive")] Course course) 
        {
           
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Level2 = new SelectList(db.CourseLevel1, "CourseLevelId", "level");
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "courseID,courseName,credits")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Course course = await db.Courses.FindAsync(id);
        //    db.Courses.Remove(course);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public ActionResult DeleteConfirmedJSON(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExportToExcel()
        {
            var gv = new GridView();
            gv.DataSource = db.Courses.ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ListOfCourses.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();



            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
