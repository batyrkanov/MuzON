using AutoMapper;
using Microsoft.Owin.Security;
using MuzON.BLL.DTO;
using MuzON.BLL.Infrastructure;
using MuzON.BLL.Interfaces;
using MuzON.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MuzON.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IUserService service) : base(service) { }

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
    }
}