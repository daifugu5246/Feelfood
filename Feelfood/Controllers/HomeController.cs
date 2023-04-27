using Feelfood.Areas.Identity.Data;
using Feelfood.Data;
using Feelfood.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Feelfood.Controllers
{
    [Authorize]   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<FeelfoodUser> _userManager;
        private readonly FeelfoodDbContext _db;

        public HomeController(ILogger<HomeController> logger, UserManager<FeelfoodUser> userManager, FeelfoodDbContext db)
        {
            _userManager = userManager;
            _logger = logger;
            _db = db;
        }

        public async Task <IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return RedirectToPage("Identity/Account/Login");
            }
            else
            {
                var firstName = user.FirstName;
                ViewData["FirstName"] = firstName;
                var lastName = user.LastName;
                ViewData["LastName"] = lastName;
                var username = user.UserName;
                ViewData["username"] = username;
                var phonenumber = user.PhoneNumber;
                ViewData["Phone Number"] = phonenumber;
                var email = user.Email;
                ViewData["Email"] = email;
            }
            var jobs = _db.Jobs.Where(u => u.UserId == user.Id).ToList();
            return View(jobs);
        }
        public IActionResult Addjob()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Addjob(JobModel job)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("Identity/Account/Login");
            }

            // Set the UserId property of the job to the ID of the authenticated user
            job.UserId = user.Id;

            _db.Jobs.Add(job);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Canceljob(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }
            var job = _db.Jobs.Find(Id);
            if (job == null)
            {
                return NotFound();
            }
            else
            {
                _db.Jobs.Remove(job);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}