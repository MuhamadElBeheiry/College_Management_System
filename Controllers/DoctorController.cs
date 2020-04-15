using Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class DoctorController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [HttpGet, ActionName("Profile")]
        public ActionResult ProfileGet(int? id)
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
            return View();
        }

        [HttpPost, ActionName("Profile")]
        public ActionResult ProfilePost(Doctor update, HttpPostedFileBase profileImg)
        {
            Doctor current = db.Doctors.Find(update.DoctorID);
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
            return RedirectToAction("Profile/" + current.DoctorID.ToString());
        }
    }
}