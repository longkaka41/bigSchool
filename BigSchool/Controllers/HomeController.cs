using BigSchool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            BigSchoolCT con = new BigSchoolCT();
            var upcommingCourse = con.Courses.Where(p => p.Datetime > DateTime.Now).OrderBy(p => p.Datetime).ToList();
            var userID = User.Identity.GetUserId();
            foreach (Course i in upcommingCourse)

            {
                ApplicationUser user =

                System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(i.LectureId);
                i.Name = user.Name;

                if (userID != null)

                {
                    i.isLogin = true;

                    Attendance find = con.Attendances.FirstOrDefault(p => p.CourseId == i.Id && p.Attendee == userID);
                    if (find == null)
                        i.isShowGoing = true;

                    Following findFollow = con.Followings.FirstOrDefault(p => p.FollowerId == userID && p.FolloweeId == i.LectureId);

                    if (findFollow == null)
                        i.isShowFollow = true;
                }
            }
            return View(upcommingCourse);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}