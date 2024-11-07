using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ddd.Controllers
{
    public class HomeController : Controller
    {
        //[Route("index")] با استفاده از این دیتا انوتیشن یو ار ال این اکشن به این صورت است localhost/index 
        //و دیگر اسم کنترل را نمایش نمیدهد و ادرسی که با کنترلر است را نمیشناسد و فقط این ادرس را میشناسد

        //اگر اکشن ما ورودی داشت باید ازین روش برای دریافت ورودی به صورت کوئری استرینگ در دیتا انوتیشن روت استفاده کرد
        //[Route("index,{name},{family},{age}")]

        public ActionResult Index()
        {
            return View();
        }
    }
}