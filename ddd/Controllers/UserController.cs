using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ddd.Models;

//این کنترلر توسط گزینه سوم منوی ساخت کنترلر ساخته شده است که تمام اکشن ها و عملکرد درون ها و همچنین ویوی آنهارا به صورت خودکار میسازد و ما فقط یک سری تغییرات جزئی مورد نیاز را می نویسیم
//گزینه دوم در منوی ساخت کنترلر علاوه بر ساخت کنترلر اکشن های آن را نیز میسازد  اما بدون کد های درون آن و همچنین ویو هارا نیز نمیسازد
namespace ddd.Controllers
{
    public class UserController : Controller
    {
        private db_dddEntities db = new db_dddEntities();

        // GET: User
        public ActionResult Index()
        {
            return View(db.T_Student.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Student t_Student = db.T_Student.Find(id);
            if (t_Student == null)
            {
                return HttpNotFound();
            }
            return View(t_Student);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,family,phone,email,registerDate,isActive,password,gender,nationalCode,imageName,age,marital")] T_Student t_Student)
        {
            if (ModelState.IsValid)
            {
                db.T_Student.Add(t_Student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_Student);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Student t_Student = db.T_Student.Find(id);
            if (t_Student == null)
            {
                return HttpNotFound();
            }
            return View(t_Student);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,family,phone,email,registerDate,isActive,password,gender,nationalCode,imageName,age,marital")] T_Student t_Student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_Student);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Student t_Student = db.T_Student.Find(id);
            if (t_Student == null)
            {
                return HttpNotFound();
            }
            return View(t_Student);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Student t_Student = db.T_Student.Find(id);
            db.T_Student.Remove(t_Student);
            db.SaveChanges();
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
