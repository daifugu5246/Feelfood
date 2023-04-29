using Feelfood.Areas.Identity.Data;
using Feelfood.Data;
using Feelfood.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks.Dataflow;

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

        public async Task<IActionResult> Index()
        {
            // Get the currently logged in user
            var user = await _userManager.GetUserAsync(this.User);
            if (user == null)
            {
                // If there is no logged in user, redirect to login page
                return RedirectToPage("Identity/Account/Login");
            }
            else
            {
                // If there is a logged in user, get and store their information in ViewData
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

            // Get the job associated with the current user, if any
            var job = _db.Jobs.Where(u => u.UserId == user.Id).FirstOrDefault();
            if (job == null)
            {
                // If there is no job associated with the current user, store null in ViewData
                ViewData["Model"] = job;
            }
            else
            {
                // If there is a job associated with the current user, store its information in ViewData
                ViewData["Model"] = job;
                ViewData["JobId"] = job.Id;
                ViewData["JobCanteen"] = job.Canteen;
                ViewData["JobStatus"] = job.Status;
                if (job.OrderId == null)
                {
                    // If there is no order associated with the job, store "-" in ViewData for each order field
                    ViewData["OrderUsername"] = "-";
                    ViewData["OrderUserPhone"] = "-";
                    ViewData["OrderRestaurant"] = "-";
                    ViewData["OrderMenu"] = "-";
                    ViewData["OrderDescription"] = "-";
                }
                else
                {
                    // If there is an order associated with the job, get and store its information in ViewData
                    var job_Ordered = _db.Jobs.Where(u => u.UserId == user.Id).Join(_db.Orders, j => j.OrderId, o => o.Id, (job, order) => new
                    {
                        Job = job,
                        Order = order,
                        OrderUser = order.User
                    }).FirstOrDefault();
                    if (job_Ordered == null || job_Ordered.OrderUser == null)
                    {
                        // If there is no associated user or order for the job, return NotFound
                        return NotFound();
                    }
                    ViewData["OrderUsername"] = job_Ordered.OrderUser.UserName;
                    ViewData["OrderUserPhone"] = job_Ordered.OrderUser.PhoneNumber;
                    ViewData["OrderRestaurant"] = job_Ordered.Order.Restaurent;
                    ViewData["OrderMenu"] = job_Ordered.Order.Menu;
                    ViewData["OrderDescription"] = job_Ordered.Order.Description;
                }
            }
            var orders = _db.Orders.Where(u => u.User == user).Join(_db.Jobs, o => o.JobId, j => j.Id, (order, job) => new
            {
                Job = job,
                Order = order,
                JobUser = job.User
            }).ToList();
            // Return the view with the stored ViewData
            return View(orders);
        }

        public IActionResult Addjob()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Addjob(JobModel job)
        {
            if (!ModelState.IsValid)
            {
                return View(job);
            }
            var user = await _userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return RedirectToPage("Identity/Account/Login");
            }
            var jobs = _db.Jobs.FirstOrDefault(u => u.UserId == user.Id);

            // Set the UserId property of the job to the ID of the authenticated user
            if (jobs == null)
            {
                job.UserId = user.Id;
                if(job.Description == null)
                {
                    job.Description = "-";
                }
                _db.Jobs.Add(job);
                _db.SaveChanges();
                var Job =_db.Jobs.FirstOrDefault(u => u.UserId == user.Id);
                if(Job == null)
                {
                    return NotFound();
                }
                user.JobId = Job.Id;
                _db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "You have complete your previous job before.");
                return View(job);
            }
            return RedirectToAction("Index");
        }

        public async Task <IActionResult> Canceljob(int? Id)
        {
            var user = await _userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return RedirectToPage("Identity/Account/Login");
            }
            if (Id == null || Id == 0)
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
                if(job.OrderId != null)
                {
                    var order = _db.Orders.Where(j => j.JobId == job.Id).FirstOrDefault();
                    if(order == null)
                    {
                        return NotFound();
                    }
                    _db.Orders.Remove(order);
                }
                user.JobId = null;
                _db.Jobs.Remove(job);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public async Task <IActionResult> Chooseorder()
        {
            var user = await _userManager.GetUserAsync(this.User);
            if(user == null)
            {
                return RedirectToPage("Identity/Account/Login");
            }
            var jobs = _db.Jobs.Where(j => j.UserId != user.Id && j.OrderId == null).Join(_db.Users, job => job.UserId, user => user.Id, (job, user) => new {
                Job = job,Username = user.UserName,Tel = user.PhoneNumber}).ToList();
            return View(jobs);
        }
        public IActionResult Addorder(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var job = _db.Jobs.Find(Id);
            if (job == null)
            {
                return NotFound();
            }
            AddorderModel model = new AddorderModel(job);
            model.Order.JobId = job.Id;
            model.Order.Canteen = model.Job.Canteen;
            return View(model);
        }
        [HttpPost]
        public async Task <IActionResult> Addorder(AddorderModel obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return RedirectToPage("Identity/Account/Login");
            }
            obj.Order.UserId = user.Id;
            var job = _db.Jobs.Find(obj.Order.JobId);
            if (job == null)
            {
                return NotFound();
            }
            if (obj.Order.Description == null)
            {
                obj.Order.Description = "-";
            }
            _db.Orders.Add(obj.Order);
            _db.SaveChanges();
            var order = _db.Orders.Where(j => j.JobId == obj.Order.JobId).FirstOrDefault();
            if (order == null)
            {
                return NotFound();
            }
            job.OrderId = order.Id;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}