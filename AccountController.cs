using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stanford_University.BusinessEntities;
using Stanford_University.Models;
using System.Security.Claims;

namespace Stanford_University
{

    
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitLogin(LoginModel model)
        {
            if (ModelState.IsValid) 
            {
                //we will send request to DB to check the user name & password
                //if we have user with user name and password, then user will be redirected to home page
                //else we will show validation message

                var dbcontex =  new Collegedbcontex();

                User userEntity =  dbcontex.Users.FirstOrDefault(p=>p.Email == model.Username && p.Password == model.Password);

                if (userEntity is null) 
                {
                    //there is no user with email & password provided
                    ModelState.AddModelError("", "Login Failed,Please validate your username & password!");
                    return View("Login", model);
                }
                 
                // User is valid and successful Login
                //string userId = userEntity.UserId.ToString();
                //string userName = userEntity.UserName;
                //string userEmail = userEntity.Email;
                

                var claims = new List<Claim>
                {
                    //new Claim(ClaimTypes.UserData, userId),
                    new Claim(ClaimTypes.NameIdentifier, userEntity.UserId.ToString()),
                    new Claim(ClaimTypes.Name, userEntity.UserName),
                    new Claim (ClaimTypes.Email, userEntity.Email ),
                    new Claim(ClaimTypes.Role, userEntity.Role)
                    

                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = false,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    IssuedUtc = DateTimeOffset.Now,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, authProperties);

                HttpContext.Session.SetString("Username", userEntity.UserName);

                if (userEntity.Role == Roles.Admin) 
                {
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    Student student = dbcontex.Students.FirstOrDefault(p => p.UserId == userEntity.UserId);
                    return RedirectToAction("StudentRo", "Student" , new { Studentid = student.StudentId });
                }

                

            }
            else
            {
                ModelState.AddModelError("", "Login Failed,Please validate your username & password!");
                return View("Login", model);
            }
            
        }

        public IActionResult Logout() 
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDeniedPage() 
        { 
            return View();
        
        }

    }
}
