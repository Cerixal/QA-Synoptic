using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SharePoint.Client.UserProfiles;
using NuGet.Protocol.Plugins;
using Quiz.Data.QA;
using Quiz.Data.Repository.IRepository;
using Quiz.Models.ViewModels;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Quiz.Web.Controllers
{
    public class LoginController : Controller
    {

        private readonly IQAUnitOfWork _qaRepo;
        private readonly IMapper _mapper;

        private const int SaltSize = 16;
        private const int HashSize = 20;

        public LoginController(IQAUnitOfWork qaRepo, IMapper mapper)
        {
            _qaRepo = qaRepo;
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1)
            };
            Response.Cookies.Append("EditCookie", "value1", cookieOptions);
            Response.Cookies.Append("ViewCookie", "value1", cookieOptions);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> LoginSubmit(UserVM model)
        {
            
            var User = await _qaRepo.User.GetAllAsync(filter: e => e.Username == model.Username);

            
            if (User.Count() != 0)
            {
                if (Verify(model.Password.ToString(), User.First().Password.ToString()))
                {
                    if (User.First().Role == "Edit")
                    {
                        CreateEditCookie(model.Username);
                    }
                    else if (User.First().Role == "View")
                    {
                        CreateViewCookie(model.Username);
                    }
                    else if (User.First().Role == "Restricted")
                    {
                        CreateRestrictedCookie(model.Username);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login");
        }

        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Fail()
        {
            return View();
        }

        public static bool Verify(string password, string hashedPassword)
        {

            // Extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Get result
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        public dynamic CreateEditCookie(string Username)
        {
            var claim = new List<Claim>
                {
                    new Claim("Username", Username)
                };

            var identity = new ClaimsIdentity(claim, "EditCookie");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync("EditCookie", claimsPrincipal);
            return true;
        }

        public dynamic CreateViewCookie(string Username)
        {
            var claim = new List<Claim>
            {
                new Claim("Username", Username)
            };
            var identity = new ClaimsIdentity(claim, "ViewCookie");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync("ViewCookie", claimsPrincipal);
            return true;
        }

        public dynamic CreateRestrictedCookie(string Username)
        {
            var claim = new List<Claim>
            {
                new Claim("Username", Username)
            };
            var identity = new ClaimsIdentity(claim, "RestrictedCookie");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync("RestrictedCookie", claimsPrincipal);
            return true;
        }
        
    }
}
