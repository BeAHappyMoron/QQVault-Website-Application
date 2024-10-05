using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QQVault.Data;
using QQVault.Models;

namespace QQVault.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryWrapper _repo;

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var feedbacks = _repo.Feedback.FindAllWithUsers()
                .OrderByDescending(f => f.CreatedAt)
                .Take(8); // Now using the specific method
            return View(feedbacks);
        }

    }
}
