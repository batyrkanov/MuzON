using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MuzON.BLL.DTO;
using MuzON.BLL.Infrastructure;
using MuzON.BLL.Interfaces;
using MuzON.Domain.Identity;
using MuzON.Domain.Interfaces;
using MuzON.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MuzON.Web.Controllers
{
    public class AccountController : BaseController
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        public AccountController(IUserService UserService) : base(UserService) { }
        
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password
                };
                ClaimsIdentity claim = await userService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return PartialView("_Registerpartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var errorList = new List<string>();
                var userDto = Mapper.Map<RegisterViewModel, UserDTO>(model);
                userDto.Role = "user";

                OperationDetails operationDetails = await userService.Create(userDto);
                if (operationDetails.Succedeed)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    errorList.Add(operationDetails.Message);
                    return Json(new { model, errorMessage = errorList, JsonRequestBehavior.AllowGet });
                }
                    
            }
            return Json(new { model, errorMessage = util.GetErrorList(ModelState.Values), JsonRequestBehavior.AllowGet });
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return PartialView("_ForgotPassword");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.GetUserByNameAsync(model.Email);
                if (user == null)
                {
                    return View("ForgotPasswordConfirmation");
                }

                var code = await userService.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account",
                                new { UserId = user.Id, code }, protocol: Request.Url.Scheme);
                var result = await SendEmail(user.Email, "Reset Password",
                                    "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                if(result)
                    return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
            }
            
            return Json(new { model, errorMessage = util.GetErrorList(ModelState.Values)}, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { model, errorMessage = util.GetErrorList(ModelState.Values), JsonRequestBehavior.AllowGet });
            }
            var user = await userService.GetUserByNameAsync(model.Email);
            if (user == null)
            {
                return Json(new { data = "userNotFound" });
            }
            var result = await userService.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Json(new { data = "success"});
            }
            ModelState.AddModelError("", result.Errors.First());
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        private async Task<bool> SendEmail(string userEmail, string messageSubject, string messageBody)
        {
            var sendState = false;
            var message = new MailMessage();
            message.To.Add(new MailAddress(userEmail));  // replace with valid value 
            message.From = new MailAddress("muzonfreemusik@gmail.com");  // replace with valid value
            message.Subject = messageSubject;
            message.Body = messageBody;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "muzonfreemusik@gmail.com",  // replace with valid value
                    Password = "muzon123muzon"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
                sendState = true;
            }
            return sendState;
        }
    }
}