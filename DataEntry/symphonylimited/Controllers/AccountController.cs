using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using symphonylimited.Models;

namespace symphonylimited.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly SymphonyContext db;
        public AccountController(SymphonyContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var AccountData = db.Fees.Include(c => c.Course).Include(c => c.Std);
            return View(AccountData.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");
            ViewBag.StdId = new SelectList(db.RegisteredStudents, "Id", "Name");


            return View();
        }
        [HttpPost]
        public IActionResult Create(Fee fee)
        {
            var data = db.RegisteredStudents.Include(o => o.Course);
            var feedata = data.FirstOrDefault(o => o.Id == fee.StdId);


            TempData["coursePrice"] = feedata.Course.Fees;
            TempData["courseTitle"] = feedata.Course.Title;


            return RedirectToAction("ConfirmPayment", fee);

        }
        [HttpGet]
        public IActionResult ConfirmPayment(Fee fee)
        {


            return View(fee);

        }
        [HttpPost]
        public IActionResult ConfirmPaymentDone(Fee fee)
        {
            var regStd = db.RegisteredStudents.FirstOrDefault(o => o.Id == fee.StdId);
            if (regStd != null)
            {
                regStd.FeeStatus = fee.Status;

                db.Update(regStd);
                db.Fees.Add(fee);
                db.SaveChanges();

            }
            return RedirectToAction("Index");


        }

      
        public IActionResult Edit(int Id)
        {
            var item = db.Fees.Find(Id);
            return View(item);

        }
  [HttpPost]
        public IActionResult Edit(Fee fee)
        {
            var regStd = db.RegisteredStudents.FirstOrDefault(o => o.Id == fee.StdId);
            if (regStd != null)
            {
                regStd.FeeStatus = fee.Status;

                db.Update(regStd);
              
                db.Fees.Update(fee);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int Id)
        {
            var item = db.Fees.Find(Id);
             return RedirectToAction();
             
        }
        [HttpPost]

        public IActionResult Delete(Fee fee)
        {

            db.Fees.Remove(fee);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }

}
