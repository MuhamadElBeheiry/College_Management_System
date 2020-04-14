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
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Student student = db.Students.Find(id);
            if(student == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["Student"] = student;
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

        [HttpPost]
        public ActionResult EditStudent(Student update)
        {
            Student current = db.Students.Find(update.StudentID);
            current.User.SSN = update.User.SSN;
            current.User.FullName = update.User.FullName;
            current.User.Gender = update.User.Gender;
            current.User.BirthDate = update.User.BirthDate;
            current.User.Phone = update.User.Phone;
            current.User.Email = update.User.Email;
            current.User.Password = update.User.Password;
            current.User.Street = update.User.Street;
            current.User.City = update.User.City;
            current.User.Country = update.User.Country;
            current.Lvl = update.Lvl;
            current.MajorDepID = update.MajorDepID;
            current.MinorDepID = update.MinorDepID;
            current.PaymentStatus = update.PaymentStatus;
            db.SaveChanges();
            return Redirect("~/Dean/ViewStudent/" + current.StudentID.ToString());
        }

        #endregion

        #region Doctor section

        [HttpGet]
        public ActionResult ViewDoctor(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["Doctor"] = doctor;
            ViewData["Departments"] = db.Departments.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddDoctor(Doctor doctor)
        {
            db.Doctors.Add(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteDoctor(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return RedirectToAction("Index");
            }
            foreach (var assignment in doctor.Assignments)
            {
                db.Assignments.Remove(assignment);
            }
            db.Users.Remove(doctor.User);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditDoctor(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["Doctor"] = doctor;
            ViewData["Departments"] = db.Departments.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult EditDoctor(Doctor update)
        {
            Doctor current = db.Doctors.Find(update.DoctorID);
            current.User.SSN = update.User.SSN;
            current.User.FullName = update.User.FullName;
            current.User.Gender = update.User.Gender;
            current.User.BirthDate = update.User.BirthDate;
            current.User.Phone = update.User.Phone;
            current.User.Email = update.User.Email;
            current.User.Password = update.User.Password;
            current.User.Street = update.User.Street;
            current.User.City = update.User.City;
            current.User.Country = update.User.Country;
            current.Department = update.Department;
            current.Salary = update.Salary;
            db.SaveChanges();
            return Redirect("~/Dean/ViewDoctor/" + current.DoctorID.ToString());
        }

        #endregion

        #region Employee section

        [HttpGet]
        public ActionResult ViewEmployee(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["Employee"] = employee;
            ViewData["Departments"] = db.Departments.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteEmployee(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }

            db.Users.Remove(employee.User);
            db.Employees.Remove(employee);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult EditEmployee(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["Doctor"] = employee;
            ViewData["Departments"] = db.Departments.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult EditEmployee(Employee update)
        {
            Employee current = db.Employees.Find(update.EmployeeID);
            current.User.SSN = update.User.SSN;
            current.User.FullName = update.User.FullName;
            current.User.Gender = update.User.Gender;
            current.User.BirthDate = update.User.BirthDate;
            current.User.Phone = update.User.Phone;
            current.User.Email = update.User.Email;
            current.User.Password = update.User.Password;
            current.User.Street = update.User.Street;
            current.User.City = update.User.City;
            current.User.Country = update.User.Country;
            current.Department = update.Department;
            current.Salary = update.Salary;
            db.SaveChanges();
            return Redirect("~/Dean/ViewEmployee/" + current.EmployeeID.ToString());
        }

        #endregion

    }
}