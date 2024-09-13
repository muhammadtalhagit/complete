using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using symphonylimited.Models;

namespace symphonylimited.Controllers
{ [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
       

        private readonly SymphonyContext db;

        public AdminController(SymphonyContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            //if(HttpContext.Session.GetString("role") == "admin")
            //{
            //    ViewBag.email = HttpContext.Session.GetString("adminEmail");
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login");
            //}
            return View();
        }

		public IActionResult Login()
		{
			return View();
		}

        
        [HttpPost]

        [ValidateAntiForgeryToken]

        public IActionResult Login(string email, string pass)
        {
            //if (email == "admin@gmail.com" && pass == "123")
            //{
            //    HttpContext.Session.SetString("adminEmail", email);
            //    HttpContext.Session.SetString("role", "admin");
            //    return RedirectToAction("Index");
            //}
            //else if (email == "user@gmail.com" && pass == "123")
            //{
            //    HttpContext.Session.SetString("userEmail", email);
            //    HttpContext.Session.SetString("role", "user");
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ViewBag.msg = "Invalid Credentials";
            //    return View();
            //}
            return View();
        }

      

        public IActionResult Aboutus()
        {
            //if (HttpContext.Session.GetString("role") == "admin")
            //{
            //    ViewBag.email = HttpContext.Session.GetString("adminEmail");
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login");
            //}
            return View();
        }


        public IActionResult Contactus()
        {
            //if (HttpContext.Session.GetString("role") == "admin")
            //{
            //    ViewBag.email = HttpContext.Session.GetString("adminEmail");
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login");
            //}
            return View();
        }


        public IActionResult academicdepartment()
        {
            var regStudentsList=db.RegisteredStudents.Include(a =>a.Course).ToList();
            ViewBag.RegisterStd = regStudentsList;

            var entranceStdList = db.EntranceStudents.Include(a =>a.Course).ToList();
            return View(entranceStdList);
        }

        public IActionResult ChangeResult(int id)
        {
            var regStudentsList = db.EntranceStudents.FirstOrDefault(p=>p.Id==id);
            ViewBag.ExamId = new SelectList(db.Exams, "ExamId", "Venue");
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");

            return View(regStudentsList);
        }

        [HttpPost]
        public IActionResult ChangeResult(EntranceStudent std)
        {
            var regStudent = db.EntranceStudents.FirstOrDefault(p => p.Id == std.Id);
           
            regStudent.Name = std.Name;
           regStudent.Email=std.Email;
            regStudent.Address= std.Address;
            regStudent.Result = std.Result;
            regStudent.CourseId = std.CourseId;
            regStudent.PhoneNo = std.PhoneNo;
            regStudent.ExamId = std.ExamId;

            db.EntranceStudents.Update(regStudent);
            db.SaveChanges();
            if(std.Result == "Pass")
            {
               var check = db.RegisteredStudents.FirstOrDefault(a=>a.Email==std.Email);
                if
                    (check == null) {
                    RegisteredStudent newstd = new RegisteredStudent()
                    { 
                        Name = std.Name,
                        Email = std.Email,
                        PhoneNo = std.PhoneNo,
                        Address = std.Address,
                        CourseId = (int)std.CourseId,
                        

                    };
                    db.RegisteredStudents.Add(newstd);
                    db.SaveChanges();
                }
               
                return RedirectToAction("academicdepartment");
            }
            

            return RedirectToAction("academicdepartment");
        }
        public IActionResult Accountdepartment()
        {
            //if (HttpContext.Session.GetString("role") == "admin")
            //{
            //    ViewBag.email = HttpContext.Session.GetString("adminEmail");
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login");
            //}
            return View();
        }

        public IActionResult Edit(int Id)
        {
            var item = db.RegisteredStudents.Find(Id);
            if (item != null)
            {
                ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");
            

                return View(item);
            }
            else
            {
                ViewBag.errorMsg = "Student is not available.";
                return RedirectToAction("academicdepartment");
            }

        }
        public IActionResult Delete(int Id)
        {
            var item = db.EntranceStudents.Find(Id);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                return RedirectToAction();
            }
        }

        [HttpPost]

        public IActionResult Delete(EntranceStudent item)
        {

            db.EntranceStudents.Remove(item);
            db.SaveChanges();

            return RedirectToAction("academicdepartment");
        }


        public IActionResult RegStdDelete(int Id)
        {
            var item = db.RegisteredStudents.Find(Id);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                return RedirectToAction();
            }
        }

        [HttpPost]

        public IActionResult RegStdDelete(RegisteredStudent item)
        {

            db.RegisteredStudents.Remove(item);
            db.SaveChanges();

            return RedirectToAction("academicdepartment");
        }




        public IActionResult LogoutAdmin()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("role");
            HttpContext.Session.Remove("adminEmail");

            return RedirectToAction("Login");

        }





    }
}
