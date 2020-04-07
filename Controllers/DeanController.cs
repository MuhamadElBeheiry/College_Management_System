using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class DeanController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            ViewData["Departments"] = db.Departments.ToList();
            ViewData["Levels"] = db.Levels.ToList();
            ViewData["Students"] = db.Students.ToList();
            ViewData["Doctors"] = db.Doctors.ToList();
            ViewData["Employees"] = db.Employees.ToList();
            return View();
        }

        #region Student section

        [HttpGet]
        public ActionResult ViewStudent(int? id)
        {
            if (id == null || db.Students.Find(id) == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["Student"] = db.Students.Find(id);
            ViewData["Departments"] = db.Departments.ToList();
            ViewData["Levels"] = db.Levels.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteStudent(int? id)
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
            foreach (var answer in student.Answers)
            {
                db.Answers.Remove(answer);
            }
            db.Users.Remove(student.User);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditStudent(int? id)
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
            ViewData["Departments"] = db.Departments.ToList();
            ViewData["Levels"] = db.Levels.ToList();
            return View();
        }

        #endregion

        #region Doctor section



        #endregion

        #region Employee section



        #endregion

    }
}