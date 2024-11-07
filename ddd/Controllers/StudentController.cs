using ddd.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace ddd.Controllers
{
    public class StudentController : Controller
    {
        db_dddEntities db = new db_dddEntities();

        public ActionResult Index()
        {
            var studentList = db.T_Student;//dont need convert t-student to list because automaticaly convert it
            return View(studentList);
        }

        [HttpGet]
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create([Bind(Include = "name,family,phone,email,nationalCode,password,gender")] T_Student student, HttpPostedFileBase imageName)//can not recive parameter one by one from student model
        {
            if (ModelState.IsValid)
            {
                string newImageName = "user.png";

                if (imageName != null)
                {
                    if (imageName.ContentType != "image/jpeg" && imageName.ContentType != "image/png")
                    {
                        ModelState.AddModelError("imageName", "تصویر شما فقط باید با فرمت png یا jpg یا jpeg باشد!");
                        return View();//وقتی درون ویو استیودنت رو  بنویسیم هنگام اجرای ویو صفحه یکبار رفرش نمیشود و اطلاعات درون فیلد ها باقی میمانند و آن فیلدی که مشکل دارد رفرش میشود و ارور میدهد
                    }

                    if (imageName.ContentLength > 300000)
                    {
                        ModelState.AddModelError("imageName", "سایز تصویر شما نباید بیشتر از 300 کیلوبایت باشد!");
                        return View();
                    }

                    newImageName = Guid.NewGuid().ToString().Replace("-", "")+Path.GetExtension(imageName.FileName);
                    imageName.SaveAs(Server.MapPath("/images/profileImages/"+newImageName));//The photo will be stored on the server, not on the database(photo name saved in data base)
                }
                student.imageName = newImageName;

                student.isActive = true;
                student.registerDate = DateTime.Now;

                db.T_Student.Add(student);
                db.SaveChanges();//for save changes after adding student(in this view after using,, dispose the add student and after that save changes)
                return RedirectToAction("index");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student =  db.T_Student.Find(id);

            if (student == null) 
            {
                return HttpNotFound();
            }

            return View(student);
        }

    }
}