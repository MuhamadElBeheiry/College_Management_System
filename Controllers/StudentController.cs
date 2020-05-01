using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Project.Controllers
{
    public class StudentController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [HttpGet, ActionName("Profile")]
        public ActionResult ProfileGet(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["Student"] = student;
            return View();
        }

        [HttpPost, ActionName("Profile")]
        public ActionResult ProfilePost(Student update, HttpPostedFileBase profileImg)
        {
            Student current = db.Students.Find(update.StudentID);
            current.User.Phone = update.User.Phone;
            current.User.Email = update.User.Email;
            current.User.Password = update.User.Password;
            current.User.Street = update.User.Street;
            current.User.City = update.User.City;
            current.User.Country = update.User.Country;

            if (profileImg != null) //check if there is anychange in profile image
            {
                if (current.User.UserPicture != null) //check if there is an old image
                {
                    string imgPath = Server.MapPath("~/" + current.User.UserPicture.Path); //get path of the old image
                    if (System.IO.File.Exists(imgPath)) //check if image file is exist
                    {
                        System.IO.File.Delete(imgPath); //delete the old image
                    }
                }
                string path = "~/Database/Images/" + Path.GetFileName(profileImg.FileName); //get path of new image
                profileImg.SaveAs(Server.MapPath(path)); //save new uploaded profile image file
                current.User.UserPicture.Path = path; //update profile image
            }

            db.SaveChanges();
            return RedirectToAction("Profile/" + current.StudentID.ToString());
        }

        [HttpGet]
        public ActionResult ViewCourses(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["Courses"] = student.Courses;
            //Redirect to the view of student's courses or edit it if you are doing it in different way.
            return View("~/Views/Student/Courses.cshtml");
        }

        [HttpGet]
        public ActionResult ViewTimetable(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return RedirectToAction("Index");
            }

            Timetable timetable = new Timetable();
            foreach (var item in db.Timetables)
            {
                if (item.Lvl == student.Lvl && item.MajorDepID == student.MajorDepID && item.MinorDepID == student.MinorDepID)
                {
                    timetable = item;
                }
            }
            ViewData["Timetable"] = timetable;
            return View();
        }
    }
}