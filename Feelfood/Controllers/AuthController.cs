using Feelfood.Data;
using Feelfood.Models;
using Microsoft.AspNetCore.Mvc;

namespace Feelfood.Controllers
{

    public class AuthController : Controller
    {
        private readonly FeelfoodDbContext _dbContext;
       public AuthController(FeelfoodDbContext dbContext)
       {
            _dbContext = dbContext;
       }
       public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Register(User user)
        {
            return View();
        }
    }
}
