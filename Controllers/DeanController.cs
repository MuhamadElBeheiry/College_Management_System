using Project.Authorization;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("Dean")]
    public class DeanController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var id = (Session["UserData"] as User).Doctor.DoctorID;
            ViewData["Dean"] = db.Doctors.Find(id);
            ViewData["Departments"] = db.Departments.ToList();
            ViewData["Levels"] = db.Levels.ToList();
            ViewData["Students"] = db.Students.ToList();
            ViewData["Doctors"] = db.Doctors.ToList();
            ViewData["Employees"] = db.Employees.ToList();
            return View();
        }

        #region Student section

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
            ViewData["Dean"] = db.Doctors.Find((Session["UserData"] as User).Doctor.DoctorID);
            ViewData["Student"] = student;
            ViewData["Departments"] = db.Departments.ToList();
            ViewData["Levels"] = db.Levels.ToList();
            return View();
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
            ViewData["Dean"] = db.Doctors.Find((Session["UserData"] as User).Doctor.DoctorID);
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
            return RedirectToAction("ViewStudent", "Dean", new { id = current.StudentID });
        }

        #endregion

        #region Doctor section

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
            ViewData["Dean"] = db.Doctors.Find((Session["UserData"] as User).Doctor.DoctorID);
            ViewData["Doctor"] = doctor;
            ViewData["Departments"] = db.Departments.ToList();
            return View();
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
            ViewData["Dean"] = db.Doctors.Find((Session["UserData"] as User).Doctor.DoctorID);
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
            return RedirectToAction("ViewDoctor", "Dean", new { id = current.DoctorID });
        }

        #endregion

        #region Employee section

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
            ViewData["Dean"] = db.Doctors.Find((Session["UserData"] as User).Doctor.DoctorID);
            ViewData["Employee"] = employee;
            ViewData["Departments"] = db.Departments.ToList();
            return View();
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
            ViewData["Dean"] = db.Doctors.Find((Session["UserData"] as User).Doctor.DoctorID);
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
            return RedirectToAction("ViewEmployee", "Dean", new { id = current.EmployeeID });
        }

        #endregion

        #region Department section

        public ActionResult ViewDepartments()
        {
            ViewData["Departments"] = db.Departments.ToList();
            return View();
        }

        public ActionResult AddDepartment(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult DeleteDepartment(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ViewDepartments");
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return RedirectToAction("ViewDepartments");
            }
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("ViewDepartments");
        }

        [HttpGet]
        public ActionResult EditDepartment(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ViewDepartments");
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return RedirectToAction("ViewDepartments");
            }
            ViewData["Department"] = department;
            return View();
        }

        [HttpPost]
        public ActionResult EditDepartment(Department update)
        {
            Department current = db.Departments.Find(update.DepartmentID);
            current.Title = update.Title;
            db.SaveChanges();
            return View();
        }

        #endregion

        #region Courses section

        public ActionResult ViewCourses()
        {
            ViewData["Courses"] = db.Courses.ToList();
            return View();
        }

        public ActionResult AddCourse(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
            return RedirectToAction("ViewCourses");
        }

        [HttpGet]
        public ActionResult DeleteCourse(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ViewCourses");
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return RedirectToAction("ViewCourses");
            }
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("ViewCourses");
        }

        [HttpGet]
        public ActionResult EditCourse(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ViewCourses");
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return RedirectToAction("ViewCourses");
            }
            ViewData["Course"] = course;
            return View();
        }

        [HttpPost]
        public ActionResult EditCourse(Course update)
        {
            Course current = db.Courses.Find(update.CourseID);
            current.DoctorID = update.DoctorID;
            current.DepID = update.DepID;
            current.Credits = update.Credits;
            current.Lvl = update.Lvl;
            current.Title = update.Title;
            db.SaveChanges();
            return Redirect("~/Dean/EditCourse/" + current.CourseID.ToString());
        }

        #endregion

    }
}