using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QQVault.Data;
using QQVault.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QQVault.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        private readonly UserManager<User> _userManager;

        public FeedbackController(IRepositoryWrapper repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        // GET: Feedback/Index
        public IActionResult Index()
        {
            var feedbacks = _repo.Feedback.FindAllWithUsers();
                 // Now using the specific method
            return View(feedbacks);
        }

        // GET: Feedback/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feedback feedback)
        {
                // Get the currently logged-in user
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    feedback.UserId = user.Id; // Set the UserId for feedback
                    feedback.CreatedAt = DateTime.Now; // Set the creation time

                    // Create the feedback
                    _repo.Feedback.Create(feedback);
                    _repo.Save();

                    return RedirectToAction("ThankYou"); // Redirect to thank you page after saving
                }

            
            return View(feedback);
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
