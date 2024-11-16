using ddd.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace ddd.Controllers
{
    public class StudentController : Controller
    {
        db_dddEntities db = new db_dddEntities();

        #region List
        public ActionResult Index()
        {
            var studentList = db.T_Student;//dont need convert t-student to list because automaticaly convert it
            return View(studentList);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create([Bind(Include = "name,family,email,phone,password,age,gender,marital")] T_Student student, HttpPostedFileBase imageName, string repeatPassword)//can not recive parameter one by one from student model
        {
            if (ModelState.IsValid)
            {
                if (student.password != repeatPassword)
                {
                    ModelState.AddModelError("password", "رمز عبور با تکرار آن مطابقت ندارد!");
                    return View(student);
                }

                string newImageName = "user.png";

                if (imageName != null)
                {
                    if (imageName.ContentType != "image/jpeg" && imageName.ContentType != "image/png")
                    {
                        ModelState.AddModelError("imageName", "تصویر شما فقط باید با فرمت png یا jpg یا jpeg باشد!");
                        return View(student);//وقتی درون ویو استیودنت رو  بنویسیم هنگام اجرای ویو صفحه یکبار رفرش نمیشود و اطلاعات درون فیلد ها باقی میمانند و آن فیلدی که مشکل دارد رفرش میشود و ارور میدهد
                    }

                    if (imageName.ContentLength > 300000)
                    {
                        ModelState.AddModelError("imageName", "سایز تصویر شما نباید بیشتر از 300 کیلوبایت باشد!");
                        return View(student);
                    }

                    newImageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageName.FileName);
                    imageName.SaveAs(Server.MapPath("/images/profileImages/" + newImageName));//The photo will be stored on the server, not on the database(photo name saved in data base)
                }
                student.imageName = newImageName;

                student.isActive = true;
                student.registerDate = DateTime.Now;

                db.T_Student.Add(student);
                db.SaveChanges();//for save changes after adding student(in this view after using,, dispose the add student and after that save changes)
                return RedirectToAction("index");
            }
            return View(student);
        }
        #endregion

        #region Details
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = db.T_Student.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }
        #endregion

        #region Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = db.T_Student.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,name,family,email,registerDate,isActive,nationalCode,imageName,phone,password,age,gender,marital")] T_Student student,HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                if (imageUpload!=null)
                {
                    if (imageUpload.ContentType != "image/jpeg" && imageUpload.ContentType != "image/png")
                    {
                        ModelState.AddModelError("imageName", "تصویر شما فقط باید با فرمت png یا jpg یا jpeg باشد!");
                        return View(student);
                    }

                    if (imageUpload.ContentLength > 300000)
                    {
                        ModelState.AddModelError("imageName", "سایز تصویر شما نباید بیشتر از 300 کیلوبایت باشد!");
                        return View(student);
                    }
                    string newImageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath("/images/profileImages/" + newImageName));
                    student.imageName = newImageName;
                }
                db.Entry(student).State=EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           return View(student);
        }





        #endregion

        #region Delete

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = db.T_Student.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")] //چون دوتا اکشن هم نام و هم ورودی داریم ارور میدهد از یک طرق باید نام دو اکشن نیز یکسان باشد پس از دیتا انوتیشن رو به رو استفاده میکنیم
        [ValidateAntiForgeryToken] // برای جلوگیری از حملاتی استفاده میشود که هکر ها سرور را با اطلاعات بی مورد یا تبلیغات پر میکنند.علاوه بر اینجا باید در ویوی این اکشن هم در قسمت بعد از یوزینگ انتی فورجری تگ اچتمل هلپر نیز نوشته شود
        //حتما باید بالای اکشن های پست نوشته شود چون اکشن های پست اطلاعات را به سرور ارسال می کنند
        public ActionResult DeleteStudent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = db.T_Student.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            if (student != null)
            {
                db.T_Student.Remove(student);
                db.SaveChanges();

                if (student.imageName != "user.png")
                {
                    if (System.IO.File.Exists(Server.MapPath("/images/profileImages") + student.imageName))
                    {
                        System.IO.File.Delete(Server.MapPath("/images/profileImages") + student.imageName);
                    }
                }

                return RedirectToAction("Index");
            }



            return View(student);
        }















        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)//بستن کانکشن های موجود هنگام خارج شدن از صفحه سایت
        {
            if (disposing)//== if(disposing==true)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}