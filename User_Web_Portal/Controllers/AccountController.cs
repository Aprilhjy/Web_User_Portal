using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using User_Web_Portal.Models.Account;
using User_Web_Portal.Models.General;
using User_Web_Portal.ViewModels.Account;
using WebMatrix.WebData;

namespace User_Web_Portal.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken ]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                bool isAuthenticated = WebSecurity.Login(loginModel.UserName, loginModel.Password, loginModel.RememberMe);

                if(isAuthenticated)
                {
                    string returnUrl = Request.QueryString["ReturnUrl"];
                    
                    if (returnUrl == null)
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        return Redirect(Url.Content(returnUrl));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password are invalid.");
                }
            }

            return View();
        }

        [HttpGet, Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            if (ModelState.IsValid)
            {
                bool isPasswordChanged = WebSecurity.ChangePassword(WebSecurity.CurrentUserName, changePassword.OldPassword, changePassword.NewPassword);

                if (isPasswordChanged)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Old Password is not correct.");
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            WebSecurity.Logout();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet, Authorize(Roles = "Administrator")]
        public ActionResult Register()
        {
            GetRolesForCurrentUser();
            return View();
        }

        private void GetRolesForCurrentUser()
        {
            if (Roles.IsUserInRole(WebSecurity.CurrentUserName, "Administrator"))
                ViewBag.RoleId = (int)Role.Administrator;
            else
                ViewBag.RoleId = (int)Role.NoRole;
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Register(RegisterModel registerModel)
        {
            GetRolesForCurrentUser();

            if (ModelState.IsValid)
            {
                bool isUserExists = WebSecurity.UserExists(registerModel.UserName);

                if (isUserExists)
                {
                    ModelState.AddModelError("UserName", "User Name already exists");
                }
                else
                {
                    WebSecurity.CreateUserAndAccount(registerModel.UserName, registerModel.Password, new { FullName = registerModel.FullName, Email = registerModel.Email });
                    Roles.AddUserToRole(registerModel.UserName, registerModel.Role);

                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return View();
        }

        [HttpGet, Authorize]
        public ActionResult UserProfileModel()
        {
            UserProfile userProfile = AccountViewModels.GetUserProfileData(WebSecurity.CurrentUserId);
            return View(userProfile);
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult UserProfileModel(UserProfile userProfile)
        {
            if(ModelState.IsValid)
            {
                AccountViewModels.UpdateUserProfile(userProfile);
                ViewBag.Message = "Profile is saved successfully.";
            }
            return View();
        }

    }
}