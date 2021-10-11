using DowntownCarClub.Web.Context;
using DowntownCarClub.Web.Dtos;
using DowntownCarClub.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DowntownCarClub.Web.Controllers
{
    [Route("/users")]
    public class UserController : Controller
    {
        private AppDbContext AppDbContext { get; }

        public UserController(
            AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> Details(string userName)
        {
            var user = await AppDbContext
                .Users
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .Include(x => x.Answers)
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Questions = x.Questions.Select(y => new QuestionDto
                    {
                        Title = y.Title,
                        Slug = y.Slug,
                        UserName = x.UserName,
                        Answers = y.Answers.Count,
                    }).ToList(),
                    Answers = x.Answers.Select(y => new QuestionDto
                    {
                        Title = y.Question.Title,
                        Slug = y.Question.Slug,
                        UserName = x.UserName,
                        Answers = y.Question.Answers.Count,
                    }).ToList(),
                })
                .FirstOrDefaultAsync(x => x.UserName == userName);

            if (user is null)
            {
                return NotFound();
            }

            var userCanLogOut = false;
            try
            {
                var currentUserId = this.GetCurrentUserId();
                if (currentUserId == user.Id)
                {
                    userCanLogOut = true;
                }
            }
            catch
            {
                userCanLogOut = false;
            }
            finally
            {
                user.UserCanLogOut = userCanLogOut;
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIsNotAuthenticated = !this.UserIsAuthenticated();
            if (userIsNotAuthenticated)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            var currentUserId = this.GetCurrentUserId();

            var user = await AppDbContext.Users.Select(x => new { x.Id, x.UserName }).FirstOrDefaultAsync(x => x.Id == currentUserId);

            if (user is null)
            {
                return RedirectToAction("LogOut", "Auth");
            }

            return RedirectToAction(nameof(Details), new { UserName = user.UserName });
        }
    }
}
