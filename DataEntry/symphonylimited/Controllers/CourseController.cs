using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using symphonylimited.Models;

namespace symphonylimited.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CourseController : Controller
    {

        private readonly SymphonyContext db;
        public CourseController(SymphonyContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var CoursesData = db.Courses.Include(c => c.RegisteredStudents);
            return View(CoursesData.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");


            return View();
        }

        [HttpPost]
 
        public IActionResult Create(Course course, IFormFile file)
        {
            var imageName = DateTime.Now.ToString("yymmddhhmmss");//24074455454454
            imageName += Path.GetFileName(file.FileName);//24074455454454apple.png

            string imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Uploads");
            var imagevalue = Path.Combine(imagepath, imageName);

            using (var stream = new FileStream(imagevalue, FileMode.Create))
            {

                file.CopyTo(stream);

            }

            var dbimage = Path.Combine("/Uploads", imageName);//   /uploads/240715343434apple.png
            course.Image = dbimage;

            db.Courses.Add(course);
            db.SaveChanges();

            ViewBag.Id = new SelectList(db.Courses, "Id", "Title");
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {
            var item = db.Courses.Find(Id);
            if (item != null)
            {
                ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");
                return View(item);
            }
            else
            {
                ViewBag.errorMsg = "Course is not available.";
                return RedirectToAction();
            }

        }

        [HttpPost]
            public IActionResult Edit(Course course, IFormFile file, string oldImage)
            {
                if (file != null && file.Length > 0)
                {
                    string imagename = DateTime.Now.ToString("yymmddhhmmss");//2410152541245412
                    imagename += "-" + Path.GetFileName(file.FileName);//2410152541245412-sonata.jpg

                    var imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Uploads");
                    var imageValue = Path.Combine(imagepath, imagename);

                    using (var stream = new FileStream(imageValue, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var dbimage = Path.Combine("/Uploads", imagename);//Uploads/2410152541245412-sonata.jpg
                    course.Image = dbimage;
                    db.Courses.Update(course);
                    db.SaveChanges();
                }
                else
                {
                    course.Image = oldImage;
                    db.Courses.Update(course);
                    db.SaveChanges();
                }

                ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");
                return RedirectToAction("Index");
                 }
         
        public IActionResult Delete(int Id)
        {
            var item = db.Courses.Find(Id);
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

        public IActionResult Delete(Course item)
        {

            db.Courses.Remove(item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Details(int Id)
        {
            var item = db.Courses.Find(Id);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                return RedirectToAction();
            }
        }

    }
}
