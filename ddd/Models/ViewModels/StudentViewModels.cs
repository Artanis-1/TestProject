﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using ddd.Models;

//ما دودسته ویو مدل داریم 1.دیتا مدل ها یا همان متادیتا ها و 2.ویو مدل های پارشیالی که اصطلاح خودم است 
//در دیتا مدل ها تمام کلاس مدلی که با دیتابیس در ارتباط است را می نویسیم برای اینکه دیتا انوتیشن هارا در کلاس مدل ننویسیم
//ویو مدل ها ی پارشیالی برای این است که یک تکه ی مورد نظر را از کلاس مدل اصلی جدا کنیم و کارهایی که میخواهیم را رویش اعمال  کنیم مثل  یک لیست جدید از دانشجویان که دیگر بعضی از فیلد هارا ندارد و ما آنهارا حذف کرده ایم



//metadata classes : (به ما این قابلیت را میدهد که یک کلاس داشته باشیم که در کنار کلاس تی استیودنت ما باشد و آن کلاس جدید ادامه دهنده ی کلاس تی استیودنت ما باشد(دو کلاس را به هم متصل کنیم
namespace ddd.Models/*.ViewModel*/ //وقتی بهش ادرس پوشه رو هم میدیم بخاطر اینکه کلاس تی استیودنت و کلاس برادرش دیگه تو یک مسیر نیستند همو نمیشناسن و دیتا انوتیشن ها اعمال نمیشن
{
    public class StudentViewModels
    {
        [Display(Name = "آیدی")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public int id { get; set; }

        [Display(Name = "نام")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0} باید حداقل 2 و حداکثر 20 کاراکتر داشته باشد")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public string name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0} باید حداقل 2 و حداکثر 20 کاراکتر داشته باشد")]
        public string family { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [RegularExpression("(09)[0-9]{9}", ErrorMessage = "لطفا شماره موبایل صحیح را وارد کنید")]
        public string phone { get; set; }

        [Display(Name = "آدرس ایمیل")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل صحیح را وارد کنید")]
        public string email { get; set; }

        [Display(Name = "تاریخ عضویت")]
        [Required(ErrorMessage = "فیلد {0} اجباری است!")]
        [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        public System.DateTime registerDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool isActive { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "فیلد {0} اجباری است!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0} باید حداقل 8 و حداکثر 20 کاراکتر داشته باشد")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "جنسیت")]
        public Nullable<bool> gender { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "فیلد {0} اجباری است!")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "لطفا {0} خود را صحیح وارد کنید")]
        public string nationalCode { get; set; }

        [Display(Name = "تصویر")]
        public string imageName { get; set; }
        [Display(Name = "سن")]
        public Nullable<int> age { get; set; }
        [Display(Name = "وضعیت تاهل")]
        public Nullable<bool> marital { get; set; }
        public StudentViewModels()
        {

        }
        public StudentViewModels(int id, string name, string family, string phone, string email, DateTime date, bool isActive)
        {
            this.id = id;
            this.name = name;
            this.family = family;
            this.phone = phone;
            this.email = email;
            this.registerDate = date;
            this.isActive = isActive;
        }
    }

    [MetadataType(typeof(StudentViewModels))] // کلاس  تی استیودنت برای اینکه با این کلاس استیودنت ویو مدل سینک بشه یا به عبارتی وصل بشه باید از دیتا انوتیشن رو به رو استفاده کنیم . و همچنین برای مثال ویو ی استیودنت لیست ما برای نمایش ویژگی های هر پراپرتی با این دستور به  ویو مدل وصل شده و از دیتا انوتیشن ها استفاده می کند
    public partial class T_Student  //در واقع این کلمه ی پارشیال است که به ما اجازه میدهد دو کلاس را به هم متصل کنیم . اسمش با خودش است کلاس را پارشیالی یا قسمت قسمت میکند
    {

    }
}
