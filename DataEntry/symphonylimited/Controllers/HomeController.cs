using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using symphonylimited.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Azure.Identity;

namespace symphonylimited.Controllers
{
	public class HomeController : Controller
	{
		  
        private readonly SymphonyContext db;
        public HomeController(SymphonyContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
		{
			var CoursesData = db.Courses.Include(c => c.RegisteredStudents);
			return View(CoursesData.ToList());
		 
        }

		public IActionResult About()
		{
            //if (HttpContext.Session.GetString("role") == "user")
            //{
            //    ViewBag.email = HttpContext.Session.GetString("userEmail");
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            return View();
        }

		public IActionResult Courses()
		{
			var CoursesData = db.Courses.Include(c => c.RegisteredStudents);
			return View(CoursesData.ToList());
		}

		public IActionResult CoursesDetails(int id)
		{
           var courses = db.Courses.Include(c => c.RegisteredStudents);
           var CoursesDetails = courses.FirstOrDefault(c => c.Id==id);
			return View(CoursesDetails);
        }
		public IActionResult EntranceExammination()
		{
            //if (HttpContext.Session.GetString("role") == "user")
            //{
            //    ViewBag.email = HttpContext.Session.GetString("userEmail");
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            return View();
        }


        [HttpPost]
        public IActionResult CheckResult(int roll)
        {
            var result = db.EntranceStudents.Find(roll);
            if (result == null)
            {
                TempData["result"] = "InValid Roll No";
                return RedirectToAction("Result");

            }
            else
            {
                TempData["result"] = result.Result;
                return RedirectToAction("Result");
            }

        }

        public IActionResult Result()
        {
            return View();

        }


        public IActionResult FAQs()
		{
            //if (HttpContext.Session.GetString("role") == "user")
            //{
            //    ViewBag.email = HttpContext.Session.GetString("userEmail");
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            return View();
        }

		public IActionResult Contact()
		{
            //if (HttpContext.Session.GetString("role") == "user")
            //{
            //    ViewBag.email = HttpContext.Session.GetString("userEmail");
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            return View();

        }


        [HttpPost]
        public IActionResult Contact(string name, string email, string message, string subject)
        {
            string msg = $"Username={name},email ={email}, subject ={subject},message ={message}.";



            if (SendEmail("arhamdev19@gmail.com", msg, "symphony contact form response."))
            {
                ViewBag.msg = "Thank you for contacting us. Our representative will call you and guide you about the further procedure";
            }
            return View();

        }

        public bool SendEmail(string email, string message, string subject)
        {

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("mhhassan.work@gmail.com", "wspe mova hpjc ppzs");

            MailMessage msg = new MailMessage("mhhassan.work@gmail.com", email);
            msg.Subject = subject;
            msg.Body = message;

            // msg.Attachments.Add(new Attachment(PathToAttachment));
            client.Send(msg);


            return true;
        }

        public IActionResult Registeredstudents()
        {
            //if (HttpContext.Session.GetString("role") == "user")
            //{
            //    ViewBag.email = HttpContext.Session.GetString("userEmail");
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            return View();

        }

        public IActionResult LogoutUser()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("role");
			HttpContext.Session.Remove("userEmail");

			return RedirectToAction("Login", "Admin");

		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
