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

namespace School.Controllers
{
    public class CourseLevelController : BaseController
    {
        private SchoolEntities db = new SchoolEntities();

        // GET: CourseLevel
        public async Task<ActionResult> Index()
        {
            return View(await db.CourseLevel1.ToListAsync());
        }

        // GET: CourseLevel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLevel1 courseLevel1 = await db.CourseLevel1.FindAsync(id);
            if (courseLevel1 == null)
            {
                return HttpNotFound();
            }
            return View(courseLevel1);
        }

        // GET: CourseLevel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseLevel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseLevelId,level")] CourseLevel1 courseLevel1)
        {
            if (ModelState.IsValid)
            {
                db.CourseLevel1.Add(courseLevel1);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(courseLevel1);
        }

        // GET: CourseLevel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLevel1 courseLevel1 = await db.CourseLevel1.FindAsync(id);
            if (courseLevel1 == null)
            {
                return HttpNotFound();
            }
            return View(courseLevel1);
        }

        // POST: CourseLevel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CourseLevelId,level")] CourseLevel1 courseLevel1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseLevel1).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(courseLevel1);
        }

        // GET: CourseLevel/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLevel1 courseLevel1 = await db.CourseLevel1.FindAsync(id);
            if (courseLevel1 == null)
            {
                return HttpNotFound();
            }
            return View(courseLevel1);
        }

        // POST: CourseLevel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CourseLevel1 courseLevel1 = await db.CourseLevel1.FindAsync(id);
            db.CourseLevel1.Remove(courseLevel1);
            await db.SaveChangesAsync();
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
