using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using Repository.Application;
using System.Collections.Generic;
using Repository.Application.DataModel;
using Web.MainApplication.WebUtility;
using Web.MainApplication.Models;
using Microsoft.Owin.Security;
using Web.MainApplication.Resources;
using Web.MainApplication.Service.System;
using System.Web;
using System.Text;
using System;
using System.DirectoryServices.AccountManagement;
using System.Web.Configuration;

namespace Web.MainApplication.Controllers
{
    public class AccountController : BaseController
    {
        //wesly simanjuntak
        private UnitOfWork unitOfWork;
        private DBEntities db = new DBEntities();

        public AccountController()
        {
            this.unitOfWork = new UnitOfWork();
        }
        public AccountController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        // GET: AccountController/Register
        public ActionResult Register()
        {
            ViewBag.IsActive = WebAppUtility.SelectListIsActive();
            return View();
        }
        public ActionResult CompleteRegister(string TokenRegister)
        {
            if (TokenRegister == null)
            {
                return HttpNotFound();
            }
            var aspNetUser = db.AspNetUsers.Where(x => x.TokenRegister == TokenRegister).FirstOrDefault();
            if (aspNetUser != null)
            {
                aspNetUser.TokenRegister = null;
                aspNetUser.IsActive = 1;
                db.Entry(aspNetUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                InfoMessagesAdd("Registration is success");
                return RedirectToAction("Login");
            }

            return HttpNotFound();


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Username,Password,Email")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userIsExist = db.AspNetUsers.Find(aspNetUsers.Username);
                    if (userIsExist != null)
                    {
                        InfoMessagesAdd("User with that username already taken");
                        return View();
                    }


                    aspNetUsers.Password = Encriptor.SHA1(aspNetUsers.Password);
                    aspNetUsers.TokenRegister = Encriptor.SHA1(aspNetUsers.Username + "fasdsfkjasldjf");
                    aspNetUsers.FullName = aspNetUsers.Username;
                    db.AspNetUsers.Add(aspNetUsers);
                    db.SaveChanges();
                    var cs = new Mail.Service1Client();

                    var listToMail = new List<string>();
                    var listCcMail = new List<string>();
                    listCcMail.Add("wesly.simanjuntak@indosuryalife.co.id");
                    listCcMail.Add("soewito.widjoyo@indosuryalife.co.id");
                    listToMail.Add(aspNetUsers.Email);

                    string baseUrl = ControllerContext.RequestContext.HttpContext.Request.Url.Scheme + "://" + ControllerContext.RequestContext.HttpContext.Request.Url.Authority;

                    string sb = "<a href=\"" + baseUrl + "/account/CompleteRegister?TokenRegister=" + aspNetUsers.TokenRegister + "\" style=\" background-color: green;" +
                        "  color: white;" +
                        "  padding: 14px 25px;" +
                        "  text-align: center;" +
                        "  text-decoration: none;" +
                        "  display: inline-block;\">Complete Registration</a>";

                    string errorMessage = string.Empty;
                    cs.SendMail(listToMail.ToArray(), "Group Health Registration", "Click this button to complete the registration " + sb, null, null, true, null, out errorMessage);
                    cs.SendMail(listCcMail.ToArray(), "New User Registration", "Dear Admin, berikut informasi user yang baru melakukan registrasi dengan username : " + aspNetUsers.Username + ".<br />Harap untuk ditindaklanjuti.", null, null, true, null, out errorMessage);
                    cs.Close();
                    SuccessMessagesAdd("Check Your Email To Complete the registration");

                    return View();
                }
                catch (Exception e)
                {

                    WarningMessagesAdd(e.MessageToList());
                }

            }

            ViewBag.IsActive = WebAppUtility.SelectListIsActive(aspNetUsers.IsActive);

            return View(aspNetUsers);
        }

        public ActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Login(LoginViewModelWebApp model)
        {
            string returnUrl = Request.Params["returnUrl"] != null ? Request.Params["returnUrl"] : null;
            string userName = model.UserName;
            string password = model.Password;

            string activeDirectoryName = WebConfigurationManager.AppSettings["ActiveDirectoryName"].ToLower();
            bool isEmailCredential = false;
            //bool isEmailCredential = WebAppUtility.IsValidEmail(userName);

            bool isISLEmployee = false;
            string usernameResult = null;
            bool ifDebug = false;
#if DEBUG
            ifDebug = true;
#endif
            if (isEmailCredential)
            {


                usernameResult = this.GetUsernameByEmailFromAD(userName);
                if (usernameResult != null)
                {
                    isISLEmployee = true;
                }
                else
                {
                    isISLEmployee = false;
                }

            }
            else
            {
                if (!ifDebug)
                {
                    if (this.IsUserExistInAD(userName))
                    {
                        isISLEmployee = true;
                    }
                    else
                    {
                        isISLEmployee = false;
                    }
                }
            }

            //Jika user karyawan ISL
            if (isISLEmployee)
            {
                if (!isEmailCredential)
                {
                    if (this.IsValidCredentialAD(userName, password))
                    {
                        var aspNetUser = this.db.AspNetUsers.Where(x => x.Username == userName).FirstOrDefault();
                        if (aspNetUser == null)
                        {
                            //Create New User To DB
                            var userPrincipal = UserPrincipalFromAD(userName);
                            var newAspNetUser = new AspNetUsers();
                            newAspNetUser.Username = userName;
                            newAspNetUser.LastLoginDate = DateTime.Now;
                            newAspNetUser.Email = userPrincipal?.EmailAddress ?? "mail@mail.com";
                            newAspNetUser.FullName = userPrincipal.Name ?? userName;
                            newAspNetUser.SetPropertyCreate();
                            db.AspNetUsers.Add(newAspNetUser);
                            db.SaveChanges();
                            userPrincipal.Dispose();
                            SignIn(this.GetClaims(newAspNetUser));
                        }
                        else
                        {
                            //Update User Info
                            aspNetUser.LastLoginDate = DateTime.Now;
                            db.Entry(aspNetUser).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            SignIn(this.GetClaims(aspNetUser));
                        }

                    }
                    else
                    {
                        WarningMessagesAdd("Password is not Match");
                    }
                }
                else
                {
                    if (this.IsValidCredentialAD(usernameResult, password))
                    {
                        var aspNetUser = this.db.AspNetUsers.Where(x => x.Username == usernameResult).FirstOrDefault();
                        if (aspNetUser == null)
                        {
                            //Create New User To DB
                            var userPrincipal = UserPrincipalFromAD(usernameResult);
                            var newAspNetUser = new AspNetUsers();
                            newAspNetUser.Username = usernameResult;
                            newAspNetUser.LastLoginDate = DateTime.Now;
                            newAspNetUser.Email = userPrincipal?.EmailAddress ?? "mail@mail.com";
                            newAspNetUser.FullName = userPrincipal.Name ?? usernameResult;
                            newAspNetUser.SetPropertyCreate();
                            db.AspNetUsers.Add(newAspNetUser);
                            db.SaveChanges();
                            userPrincipal.Dispose();
                            SignIn(this.GetClaims(newAspNetUser));
                        }
                        else
                        {
                            //Update User Info
                            aspNetUser.LastLoginDate = DateTime.Now;
                            db.Entry(aspNetUser).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            SignIn(this.GetClaims(aspNetUser));
                        }

                    }
                    else
                    {
                        WarningMessagesAdd("Password is not Match");
                    }
                }

            }
            // Jika bukan karyawan ISL
            else
            {
                if (!isEmailCredential)
                {
                    var user = db.AspNetUsers.ToList().Where(x => x.Username == userName).FirstOrDefault();
                    if (user == null)
                    {
                        WarningMessagesAdd("Username is not Exist");
                    }
                    else
                    {
                        if (user.Password != Encriptor.SHA1(password))
                        {
                            WarningMessagesAdd("Password is not Match");
                        }
                        else
                        {
                            //Berhasil Login
                            //Update User Info
                            user.LastLoginDate = DateTime.Now;
                            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            SignIn(this.GetClaims(user));
                        }
                    }
                }
                else
                {
                    var emailAddress = userName;
                    var user = db.AspNetUsers.ToList().Where(x => x.Email.ToLower() == emailAddress).FirstOrDefault();
                    if (user == null)
                    {
                        WarningMessagesAdd("Username is not Exist");
                    }
                    else
                    {
                        if (user.Password != Encriptor.SHA1(password))
                        {
                            WarningMessagesAdd("Password is not Match");
                        }
                        else
                        {
                            //Berhasil Login
                            //Update User Info
                            user.LastLoginDate = DateTime.Now;
                            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            SignIn(this.GetClaims(user));
                        }
                    }
                }
            }

            //if (userName.ToLower().Contains("@"+activeDirectoryName))
            //{
            //    userName = this.GetUsernameByEmailFromAD(userName);
            //    if (userName == null)
            //    {
            //        WarningMessagesAdd("Email is not Exist");
            //    }
            //}
            //else
            //{
            //    if (!this.IsUserExistInAD(userName))
            //    {
            //        WarningMessagesAdd("Username is not Exist");
            //    }
            //}


            //if (!Warn())
            //{
            //    if (this.IsValidCredentialAD(userName, password))
            //    {
            //        var aspNetUser = this.db.AspNetUsers.Where(x => x.Username == userName).FirstOrDefault();
            //        if (aspNetUser == null)
            //        {
            //            //Create New User To DB
            //            var userPrincipal = UserPrincipalFromAD(userName);
            //            var newAspNetUser = new AspNetUsers();
            //            newAspNetUser.Username = userName;
            //            newAspNetUser.LastLoginDate = DateTime.Now;
            //            newAspNetUser.Email = userPrincipal?.EmailAddress ?? "mail@mail.com";
            //            newAspNetUser.FullName = userPrincipal.Name ?? userName;
            //            newAspNetUser.SetPropertyCreate();
            //            db.AspNetUsers.Add(newAspNetUser);
            //            db.SaveChanges();
            //            userPrincipal.Dispose();
            //            SignIn(this.GetClaims(newAspNetUser));
            //        }
            //        else
            //        {
            //            //Update User Info
            //            aspNetUser.LastLoginDate = DateTime.Now;
            //            db.Entry(aspNetUser).State = System.Data.Entity.EntityState.Modified;
            //            db.SaveChanges();
            //            SignIn(this.GetClaims(aspNetUser));
            //        }

            //    }
            //    else
            //    {
            //        WarningMessagesAdd("Password is not Match");
            //    }
            //}
            if (WarningMessages().Count > 0)
            {
                var temp = WarningMessages();
                return View(model);
            }



            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private bool IsUserExistInAD(string username)
        {
            string activeDirectoryName = WebConfigurationManager.AppSettings["ActiveDirectoryName"];
            using (var domainContext = new PrincipalContext(ContextType.Domain, activeDirectoryName))
            {
                using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, username))
                {
                    return foundUser != null;
                }
            }
        }

        private UserPrincipal UserPrincipalFromAD(string username)
        {
            UserPrincipal userPricipal;
            string activeDirectoryName = WebConfigurationManager.AppSettings["ActiveDirectoryName"];
            var domainContext = new PrincipalContext(ContextType.Domain, activeDirectoryName);

            var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, username);
            userPricipal = foundUser;
            //domainContext.Dispose();
            return userPricipal;
        }
        private bool IsValidCredentialAD(string usernameOrEmail, string password)
        {

            bool isValid = false;
            string activeDirectoryName = WebConfigurationManager.AppSettings["ActiveDirectoryName"];

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, activeDirectoryName))
            {
                isValid = pc.ValidateCredentials(usernameOrEmail, password);
            }
            return isValid;
        }
        private string GetUsernameByEmailFromAD(string emailAddress)
        {
            string username = null;
            string activeDirectoryName = WebConfigurationManager.AppSettings["ActiveDirectoryName"];
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain, activeDirectoryName))
            {
                UserPrincipal qbeUser = new UserPrincipal(context);
                qbeUser.EmailAddress = emailAddress;

                // create your principal searcher passing in the QBE principal    
                PrincipalSearcher srch = new PrincipalSearcher(qbeUser);

                var findAll = srch.FindAll().ToList();
                // find all matches
                foreach (var found in srch.FindAll())
                {
                    username = found.SamAccountName;
                }
            }
            return username;
        }
        public ActionResult ForgotPassword(string email)
        {
            if (Request.RequestType == "POST")
            {


                var isSetNewPassword = Request.Params["isSetNewPassword"];
                if (isSetNewPassword != null && Convert.ToBoolean(isSetNewPassword))
                {
                    var newPassword = Request.Params["NewPassword"];
                    var retypeNewPassword = Request.Params["RetypeNewPassword"];
                    if (newPassword == null || retypeNewPassword == null)
                    {
                        WarningMessagesAdd("Password or ReType Password can not be null");

                    }
                    if (newPassword != retypeNewPassword)
                    {
                        WarningMessagesAdd("Password and ReType Password must be match");
                    }
                    if (WarningMessages().Count == 0)
                    {
                        var forgotToken = Request.Params["ForgotToken"];
                        var userToUpdate = db.AspNetUsers.Where(x => x.TokenForgotPassword == forgotToken).FirstOrDefault();
                        userToUpdate.Password = Encriptor.SHA1(newPassword);
                        userToUpdate.TokenForgotPassword = null;
                        userToUpdate.SetPropertyUpdate();
                        db.Entry(userToUpdate).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        SuccessMessagesAdd("Your password has been reset");
                        return RedirectToAction("LogOff");
                    }
                    else
                    {
                        var tokenForgotPassword = Request.Params["ForgotToken"];
                        var userToUpdate = db.AspNetUsers.Where(x => x.TokenForgotPassword == tokenForgotPassword).FirstOrDefault();
                        if (userToUpdate != null)
                        {
                            ViewBag.SetNewPassword = true;
                            ViewBag.ForgotToken = tokenForgotPassword;
                            return View("_ForgotPassword", userToUpdate);

                        }
                    }

                }


                var user = db.AspNetUsers.Where(x => x.Email == email).FirstOrDefault();
                if (user == null)
                {
                    WarningMessagesAdd("There wasn't an account for that email");
                    user = new AspNetUsers();
                    user.Email = email;
                    return View("_ForgotPassword", user);
                }
                else if (WebAppUtility.IsValidEmail(email) && this.GetUsernameByEmailFromAD(email) != null)
                {
                    WarningMessagesAdd("Call Your Application Admin");
                    return View("_ForgotPassword");
                }
                else
                {
                    user.TokenForgotPassword = Encriptor.SHA1(WebAppUtility.RandomString(9) + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + user.Email);
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    var cs = new Mail.Service1Client();
                    string errorMessage = string.Empty;
                    var to = new string[1];
                    to[0] = user.Email;
                    string baseUrl = ControllerContext.RequestContext.HttpContext.Request.Url.Scheme + "://" + ControllerContext.RequestContext.HttpContext.Request.Url.Authority;

                    //string sb = "<a href=\"" + baseUrl + "/account/ForgotPassword?forgotPasswordToken=" + user.TokenForgotPassword + "\" style=\" background-color: green;" +
                    //"  color: white;" +
                    //"  padding: 14px 25px;" +
                    //"  text-align: center;" +
                    //"  text-decoration: none;" +
                    //"  display: inline-block;\">Create New Password</a>";


                    string sb = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">" +
                    "  <tr>" +
                    "      <td>" +
                    "          <table cellspacing=\"0\" cellpadding=\"0\">" +
                    "              <tr>" +
                    "                  <td style=\"border-radius: 2px;\" bgcolor=\"#ED2939\">" +
                    "                      <a href=\"" + baseUrl + "/account/ForgotPassword?forgotPasswordToken=" + user.TokenForgotPassword + "\" style=\"padding: 8px 12px; border: 1px solid #ED2939;border-radius: 2px;font-family: Helvetica, Arial, sans-serif;font-size: 14px; color: #ffffff;text-decoration: none;font-weight:bold;display: inline-block;\">" +
                    "                          Your username is \"" + user.Username + "\", click this button to change your password !         " +
                    "                      </a>" +
                    "                  </td>" +
                    "              </tr>" +
                    "          </table>" +
                    "      </td>" +
                    "  </tr>" +
                    "</table>";

                    cs.SendMail(to, "Password, Group Health", sb, null, null, true, null, out errorMessage);
                    cs.Close();
                    ViewBag.SentToEmail = true;
                    SuccessMessagesAdd("Check your email to set new password");
                    return View("_ForgotPassword");
                }

            }
            if (Request.Params["forgotPasswordToken"] != null)
            {
                var tokenForgotPassword = Request.Params["forgotPasswordToken"];
                var user = db.AspNetUsers.Where(x => x.TokenForgotPassword == tokenForgotPassword).FirstOrDefault();
                if (user != null)
                {
                    ViewBag.SetNewPassword = true;
                    ViewBag.ForgotToken = tokenForgotPassword;
                    return View("_ForgotPassword", user);

                }
                WarningMessagesAdd("Link expired");

            }

            InfoMessagesAdd("Fill your email to reset password");
            return View("_ForgotPassword");
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        private void SignIn(List<Claim> claims)//Mind!!! This is System.Security.Claims not WIF claims
        {

            var claimsIdentity = new WebClaimIdentity(claims, SystemResources.DefaultAuthenticationTypes_ApplicationCookie);

            //This uses OWIN authentication

            AuthenticationManager.SignOut(SystemResources.DefaultAuthenticationTypes_ExternalCookie);



            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, claimsIdentity);


            HttpContext.User = new WebAppClaimPrincipal(AuthenticationManager.AuthenticationResponseGrant.Principal);


        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public List<Claim> GetClaims(AspNetUsers aspNetUser)
        {
            var claims = new List<Claim>();


            List<string> roles = new List<string>();
            List<int> userRolesId = new List<int>();
            List<string> menuList = new List<string>();

            foreach (var groupUser in aspNetUser.AspNetGroupUser)
            {
                foreach (var item in groupUser.AspNetGroups.AspNetRoleGroup)
                {
                    userRolesId.Add(item.RolesId);
                    if (item.AspNetRoles.Type.ToLower() == "function")
                    {
                        roles.Add(item.AspNetRoles.AspNetRoles2.Name + "/" + item.AspNetRoles.Name);
                        menuList.Add(item.AspNetRoles.AspNetRoles2.Name + "/" + item.AspNetRoles.Name + "/" + item.AspNetRoles.AspNetMenu.FirstOrDefault()?.MenuId);
                    }
                }

            }
            userRolesId = userRolesId.Distinct().ToList();
            SystemResources.DefaultRole.ToLower().Split(';').ToList().ForEach(x =>
            {
                roles.Add(x);
            });

            roles = roles.Distinct().ToList();
            roles = roles.OrderBy(x => x).ToList();
            if (string.IsNullOrEmpty(aspNetUser.Email) == false)
            {
                claims.Add(new Claim(ClaimTypes.Email, aspNetUser.Email));
            }
            if (string.IsNullOrEmpty(aspNetUser.FullName) == false)
            {
                claims.Add(new Claim(WebClaimIdentity.FullNameType, aspNetUser.FullName));
            }

            if (string.IsNullOrEmpty(aspNetUser.Username) == false)
            {
                claims.Add(new Claim(ClaimTypes.Name, aspNetUser.Username));
                claims.Add(new Claim(WebClaimIdentity.Username, aspNetUser.Username));
            }

            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.ToLower()));
            }

            var userMenu = "";

            db.AspNetMenu.Where(x => x.MenuLevel == 1 && x.IsActive == 1).OrderBy(x => x.Sequence).ToList().ForEach(x =>
              {
                  userMenu += GenerateUL(x, userRolesId);
              });
            claims.Add(new Claim(WebClaimIdentity.MenuString, userMenu));
            foreach (var item in menuList)
            {
                claims.Add(new Claim(WebClaimIdentity.Menu, item));
            }

            //Add Claim For LastVersionApplication Data
            var lastVersion = db.CommonListValue.Where(x => x.CommonListValue2.Value == VersionApplication.ApplicationVersionsSequence).OrderByDescending(x => x.Id).FirstOrDefault();
            if (lastVersion != null)
            {
                claims.Add(new Claim(WebClaimIdentity.ApplicationVersion, lastVersion.Text));
            }

            //Add Claim For DatabaseName
            var showDatabaseName = db.CommonListValue.Where(x => x.Text == AplicationConfig.ShowDatabaseName).FirstOrDefault();
            if (showDatabaseName != null)
            {
                if (showDatabaseName.Value.ToLower() == "true")
                {
                    claims.Add(new Claim(WebClaimIdentity.DatabaseName, db.Database.Connection.DataSource + " - " + db.Database.Connection.Database));

                }
            }
            //Add Claim For Skin
            claims.Add(new Claim(WebClaimIdentity.UserCSSSkin, aspNetUser.User_Skin ?? "skin-purple-light"));

            //Add Claim For EmpId


            return claims;
        }
        private string GenerateUL(AspNetMenu menu, List<int> userRolesId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");

            if (menu.IsActive == 1 && menu.ShowAsChild == 1 && menu.AspNetMenu1.Count > 0 && ContainsChildMenuOwnedByUser(menu.AspNetMenu1.Where(x => x.ShowAsChild == 1).ToList(), userRolesId))
            {
                sb.Append("<li class=\"nav-item\">");
                sb.Append("                            <a class=\"nav-link collapsed\" href=\"#navbar_" + menu.MenuId + "\" data-toggle=\"collapse\" role=\"button\" aria-expanded=\"false\" aria-controls=\"navbar-dashboards\">");
                sb.Append("                                <i class=\"" + menu.MenuIClass + "\"></i>");
                sb.Append("                                <span class=\"nav-link-text\">" + menu.MenuName + "</span>");
                sb.Append("                            </a>");
                sb.Append("                            <div class=\"collapse\" id=\"navbar_" + menu.MenuId + "\" style=\"\">");
                sb.Append("                                <ul class=\"nav nav-sm flex-column\">");
                foreach (var menuItem in menu.AspNetMenu1.OrderBy(x => x.Sequence))
                {
                    sb.Append(GenerateUL(menuItem, userRolesId));
                }
                sb.Append("                                </ul>");
                sb.Append("                            </div>");
                sb.Append("                        </li>");

                //sb.AppendLine("<li class=\"nav-item\" id=\"li_" + menu.MenuName + "\">");
                //sb.AppendLine("<a href=\"#\">");
                //sb.AppendLine("<i class=\"" + menu.MenuIClass + "\"></i>");
                //sb.AppendLine("<span>" + menu.MenuName + "</span>");
                //sb.AppendLine("<span class=\"pull-right-container\"></span>");
                //sb.AppendLine("<i class=\"fa fa-angle-left pull-right\"></i>");
                //sb.AppendLine("</span>");
                //sb.AppendLine("</a>");
                //sb.AppendLine("<ul class=\"treeview-menu\" id=\"ul_" + menu.MenuName + "\">");
                //foreach (var menuItem in menu.AspNetMenu1.OrderBy(x => x.Sequence))
                //{
                //    sb.Append(GenerateUL(menuItem, userRolesId));
                //}
                //sb.AppendLine("</ul>");
                //sb.AppendLine("</li>");
            }
            else
            {
                if (menu.IsActive == 1 && menu.ShowAsChild == 1 && menu.AspNetRoles1 != null && userRolesId.Exists(x => x == menu.AspNetRoles1.Id))
                {
                    string handler = menu.AspNetRoles1.ParentId != null ? menu.AspNetRoles1.AspNetRoles2.Name.ToLower() + "/" + menu.AspNetRoles1.Name.ToLower() : menu.AspNetRoles1.Name.ToLower();
                    string menuText = menu.MenuName;
                    string classVal = menu.MenuIClass;
                    var urlBase = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                    string id = "";

                    if (menu.AspNetRoles1.Type == "Controller")
                    {
                        id = menu.AspNetRoles1.Name + "_Index";
                    }
                    else
                    {
                        id = menu.AspNetRoles1.ParentId != null ? menu.AspNetRoles1.AspNetRoles2.Name.ToLower() + "_" + menu.AspNetRoles1.Name.ToLower() : menu.AspNetRoles1.Name.ToLower();
                    }

                    string line = String.Format(@"<li class=""{5}"" id=""{4}""><a href=""{3}{0}"" class=""{6}""><i class=""{2}""></i><span>{1}</span></a></li>", handler, menuText, classVal, urlBase, id.ToLower(), "nav-item", "nav-link");
                    sb.Append(line);
                }

            }

            sb.Append("");
            return sb.ToString();

        }
        private bool ContainsChildMenuOwnedByUser(List<AspNetMenu> listMenu, List<int> role)
        {
            foreach (var item in listMenu)
            {
                if (item.AspNetRoles1 != null && role.Contains(Convert.ToInt32(item.AspNetRoles)))
                {
                    return true;
                }
            }
            return false;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}