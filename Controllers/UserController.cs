using MvcTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcTest.Controllers
{
    public class UserController : Controller
    {
        //Registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //Registration Post Action
        [HttpPost]
        public ActionResult Registration([Bind(Exclude = "IsEmailverified,ActivationCode")] UserRegistration userRegistration)
        {
            bool Status = false;
            string message = "";
            //
            //model validation
            if (ModelState.IsValid)
            {
                #region Email is already Exist
                var isExist = IsEmailExist(userRegistration.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(userRegistration);
                }
                #endregion
                #region Generate Activation code
                userRegistration.ActivationCode = Guid.NewGuid();
                #endregion
                #region Password Hashing
                userRegistration.Password = Crypto.Hash(userRegistration.Password);
                //userRegistration.ConfirmPassword = Crypto.Hash(userRegistration.Password);
                #endregion
                userRegistration.IsEmailVerified = true;
                #region Save data to Database
                using (intissuesEntities2 db = new intissuesEntities2())
                {
                    db.UserRegistrations.Add(userRegistration);
                    db.SaveChanges();
                }

                //Send Email to User
                SendverificationLinkEmail(userRegistration.EmailID, userRegistration.ActivationCode.ToString());
                message = "Registration Successfully done,Account activation Link " +
                    "has been sent to your Email Id:" + userRegistration.EmailID;
                Status = true;
                #endregion

            }
            else
            {
                message = "Invalid Request";
            }


            ViewBag.Message = message;
            ViewBag.Status = Status;


            return View(userRegistration);
        }
        //Verify Account
        [HttpGet]
        public ActionResult VerifyAccount(string Id)
        {
            bool Status = false;
            using (intissuesEntities2 db = new intissuesEntities2())
            {
                db.Configuration.ValidateOnSaveEnabled = false; //This line I have added to avoid
                                                                // confirm password does not match issue on save changes
                var v = db.UserRegistrations.Where(a => a.ActivationCode == new Guid(Id)).FirstOrDefault();
                if(v != null)
                {
                    v.IsEmailVerified = true;
                    db.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";    
                }
            }
            ViewBag.Status = Status;
                return View();
        }
        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }   
        //Login Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login,string ReturnUrl="")
        {
            string message = "";
            using (intissuesEntities2 db = new intissuesEntities2())
            {
                var v = db.UserRegistrations.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
                if(v != null)
                {
                    if(string.Compare(Crypto.Hash(login.password),v.Password)==0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if(Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Invalid Credential Provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
                return View();
        }
        //Logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }



        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (intissuesEntities2 db = new intissuesEntities2())
            {
                var v = db.UserRegistrations.Where(s => s.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }
        [NonAction]
        public void SendverificationLinkEmail(string emailID, string activationcode)
        {
            //    var scheme = Request.Url.Scheme;
            //    var host = Request.Url.Host;
            //    var port = Request.Url.Port; 

            var verifyUrl = "/User/VerifyAccount/" + activationcode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("shivahunks.riders@gmail.com", "Timesheet");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Shiva@2121"; //Replace with actual password
            string subject = "Your account is successfully created";
            string body = "<br/><br/>we are excited to tell you that your Timesheet Account is" +
                "successfully created .Please check on the below link to verify your account" +
                "<br/><br/><a href='" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(/*"shivahunks.riders@gmail.com","Shiva@2121","smtp.gmail.com"*/fromEmail.Address, fromEmailPassword)

            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })
                smtp.Send(message);
        }
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}