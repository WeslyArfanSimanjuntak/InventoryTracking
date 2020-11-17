using Microsoft.Owin.Security.OAuth;
using Repository.Application.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using Web.MainApplication.Controllers;
using Web.MainApplication.Resources;
using Web.MainApplication.Service.System;

namespace Web.MainApplication.WebUtility
{
    public class WebOAuthAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); // 
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            using (var db = new DBEntities())
            {
                try
                {
                    string userName = context.UserName;
                    string password = context.Password;

                    string activeDirectoryName = WebConfigurationManager.AppSettings["ActiveDirectoryName"].ToLower();
                    bool isEmailCredential = WebAppUtility.IsValidEmail(userName);
                    bool isISLEmployee = false;
                    string usernameResult = null;
                    if (isEmailCredential)
                    {
                        usernameResult = WebAppUtility.GetUsernameByEmailFromAD(userName);
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
                        if (WebAppUtility.IsUserExistInAD(userName))
                        {
                            isISLEmployee = true;
                        }
                        else
                        {
                            isISLEmployee = false;
                        }
                    }
                    //Jika user karyawan ISL
                    if (isISLEmployee)
                    {
                        if (!isEmailCredential)
                        {
                            if (WebAppUtility.IsValidCredentialAD(userName, password))
                            {
                                var aspNetUser = db.AspNetUsers.Where(x => x.Username == userName).FirstOrDefault();
                                if (aspNetUser == null)
                                {
                                    //Create New User To DB
                                    var userPrincipal = WebAppUtility.UserPrincipalFromAD(userName);
                                    var newAspNetUser = new AspNetUsers();
                                    newAspNetUser.Username = userName;
                                    newAspNetUser.LastLoginDate = DateTime.Now;
                                    newAspNetUser.Email = userPrincipal?.EmailAddress ?? "mail@mail.com";
                                    newAspNetUser.FullName = userPrincipal.Name ?? userName;
                                    newAspNetUser.SetPropertyCreate();
                                    db.AspNetUsers.Add(newAspNetUser);
                                    db.SaveChanges();
                                    userPrincipal.Dispose();
                                    var claims = this.GetClaims(newAspNetUser);
                                    identity.AddClaims(claims);
                                    context.Validated(identity);
                                }
                                else
                                {
                                    //Update User Info
                                    aspNetUser.LastLoginDate = DateTime.Now;
                                    db.Entry(aspNetUser).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                    var claims = this.GetClaims(aspNetUser);
                                    identity.AddClaims(claims);
                                    context.Validated(identity);
                                }

                            }
                            else
                            {
                                context.SetError("invalid_grant", "Provided username and password is incorrect");
                                return;
                            }
                        }
                        else
                        {
                            if (WebAppUtility.IsValidCredentialAD(usernameResult, password))
                            {
                                var aspNetUser = db.AspNetUsers.Where(x => x.Username == usernameResult).FirstOrDefault();
                                if (aspNetUser == null)
                                {
                                    //Create New User To DB
                                    var userPrincipal = WebAppUtility.UserPrincipalFromAD(usernameResult);
                                    var newAspNetUser = new AspNetUsers();
                                    newAspNetUser.Username = usernameResult;
                                    newAspNetUser.LastLoginDate = DateTime.Now;
                                    newAspNetUser.Email = userPrincipal?.EmailAddress ?? "mail@mail.com";
                                    newAspNetUser.FullName = userPrincipal.Name ?? usernameResult;
                                    newAspNetUser.SetPropertyCreate();
                                    db.AspNetUsers.Add(newAspNetUser);
                                    db.SaveChanges();
                                    userPrincipal.Dispose();
                                    var claims = this.GetClaims(aspNetUser);
                                    identity.AddClaims(claims);
                                    context.Validated(identity);
                                }
                                else
                                {
                                    //Update User Info
                                    aspNetUser.LastLoginDate = DateTime.Now;
                                    db.Entry(aspNetUser).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                    var claims = this.GetClaims(aspNetUser);
                                    identity.AddClaims(claims);
                                    context.Validated(identity);
                                }

                            }
                            else
                            {
                                context.SetError("invalid_grant", "Provided username and password is incorrect");
                                return;
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
                                context.SetError("invalid_grant", "Provided username and password is incorrect");
                                return;
                            }
                            else
                            {
                                if (user.Password != Encriptor.SHA1(password))
                                {
                                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                                    return;
                                }
                                else
                                {
                                    //Berhasil Login
                                    //Update User Info
                                    user.LastLoginDate = DateTime.Now;
                                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                    var claims = this.GetClaims(user);
                                    identity.AddClaims(claims);
                                    context.Validated(identity);
                                }
                            }
                        }
                        else
                        {
                            var emailAddress = userName;
                            var user = db.AspNetUsers.ToList().Where(x => x.Email.ToLower() == emailAddress).FirstOrDefault();
                            if (user == null)
                            {
                                context.SetError("invalid_grant", "Provided username and password is incorrect");
                                return;
                            }
                            else
                            {
                                if (user.Password != Encriptor.SHA1(password))
                                {
                                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                                    return;
                                }
                                else
                                {
                                    //Berhasil Login
                                    //Update User Info
                                    user.LastLoginDate = DateTime.Now;
                                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                    var claims = this.GetClaims(user);
                                    identity.AddClaims(claims);
                                    context.Validated(identity);
                                }
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    context.SetError("invalid_grant", e.Message);
                    return;
                }
            }


        }
        private List<Claim> GetClaims(AspNetUsers aspNetUser)
        {
            var db = new DBEntities();
            var claims = new List<Claim>();

            List<string> roles = new List<string>();
            List<int> userRolesId = new List<int>();
            List<string> menuList = new List<string>();

            foreach (var groupUser in aspNetUser.AspNetGroupUser)
            {
                foreach (var item in groupUser.AspNetGroups.AspNetRoleGroup)
                {
                    userRolesId.Add(item.RolesId);
                    if (item.AspNetRoles.Type.ToLower() == "function" && item.AspNetRoles.AspNetRoles2.Type.ToLower() == "controller api")
                    {
                        roles.Add(item.AspNetRoles.AspNetRoles2.Name + "/" + item.AspNetRoles.Name);
                        menuList.Add(item.AspNetRoles.AspNetRoles2.Name + "/" + item.AspNetRoles.Name + "/" + item.AspNetRoles.AspNetMenu.FirstOrDefault()?.MenuId);
                    }
                }

            }
            userRolesId = userRolesId.Distinct().ToList();
            //SystemResources.DefaultRole.ToLower().Split(';').ToList().ForEach(x =>
            //{
            //    roles.Add(x);
            //});

            roles = roles.Distinct().ToList();
            roles = roles.OrderBy(x => x).ToList();
            //if (string.IsNullOrEmpty(aspNetUser.Email) == false)
            //{
            //    claims.Add(new Claim(ClaimTypes.Email, aspNetUser.Email));
            //}
            //if (string.IsNullOrEmpty(aspNetUser.FullName) == false)
            //{
            //    claims.Add(new Claim(WebClaimIdentity.FullNameType, aspNetUser.FullName));
            //}

            //if (string.IsNullOrEmpty(aspNetUser.Username) == false)
            //{
            //    claims.Add(new Claim(ClaimTypes.Name, aspNetUser.Username));
            //    claims.Add(new Claim(WebClaimIdentity.Username, aspNetUser.Username));
            //}

            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.ToLower()));
            }

            //Add Claim For LastVersionApplication Data
            //var lastVersion = db.CommonListValue.Where(x => x.CommonListValue2.Value == VersionApplication.ApplicationVersionsSequence).OrderByDescending(x => x.Id).FirstOrDefault();
            //if (lastVersion != null)
            //{
            //    claims.Add(new Claim(WebClaimIdentity.ApplicationVersion, lastVersion.Text));
            //}

            //Add Claim For DatabaseName
            //var showDatabaseName = db.CommonListValue.Where(x => x.Text == AplicationConfig.ShowDatabaseName).FirstOrDefault();
            //if (showDatabaseName != null)
            //{
            //    if (showDatabaseName.Value.ToLower() == "true")
            //    {
            //        claims.Add(new Claim(WebClaimIdentity.DatabaseName, db.Database.Connection.DataSource + " - " + db.Database.Connection.Database));

            //    }
            //}
            //Add Claim For Skin
            //claims.Add(new Claim(WebClaimIdentity.UserCSSSkin, aspNetUser.User_Skin ?? "skin-purple-light"));
            return claims;
        }


    }
}