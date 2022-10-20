using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc;
using Quiz.Data.QA;
using Quiz.Data.Repository.IRepository;
using Quiz.Models.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace Quiz.Web.Areas.Quiz.Controllers
{
    public class UserController : Controller
    {
        private readonly IQAUnitOfWork _qaRepo;
        private readonly IMapper _mapper;

        public UserController(IQAUnitOfWork qaRepo, IMapper mapper)
        {
            _qaRepo = qaRepo;
            _mapper = mapper;
        }

        // GET: HomeController
        public async Task<ActionResult> UserTable()
        {
            if (HttpContext.Request.Cookies.ContainsKey("EditCookie"))
            {
                DateTime? fromDate = DateTime.Now.AddDays(-365).Date;
                DateTime? toDate = DateTime.Now.Date;

                if (toDate < fromDate) (toDate, fromDate) = (fromDate, toDate);

                ViewBag.fromDate = fromDate;
                ViewBag.toDate = toDate;


                //if (toDate.Value.DayOfWeek == DayOfWeek.Thursday) toDate.Value.AddDays(4);
                //if(fromDate.Value.DayOfWeek == DayOfWeek.Thursday) fromDate.Value.AddDays(-4);
                try
                {
                    // Get the prices back from the database

                    IEnumerable<User> users = await _qaRepo.User.GetAllAsync();

                    // If prices are found return view
                    UserVM userTableVm = new();

                    if (users.Any())
                    {
                        userTableVm.Users = new List<UserVM>();
                        foreach (var user in users)
                        {
                            UserVM UserTable = _mapper.Map<User, UserVM>(user);
                            userTableVm.Users.Add(UserTable);
                        }

                        return View(userTableVm);
                    }
                    // No prices found set the message to display
                    ViewData["NoPricesFoundMessage"] = "No values found, please select a new range";

                    //Empty model to make the resulting view clean and stop
                    // null ref exception.
                    return View(userTableVm);
                }

                catch (DbException ex)
                {
                    ModelState.AddModelError("", "Error connecting to the database,"
                                                 + "please try again and if the problem persists contact Tom.");
                    return View();
                }
            }
            else
            {
                return (RedirectToAction("login", "login"));
            }
        }
    
        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
