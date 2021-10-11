using DowntownCarClub.Web.Context;
using DowntownCarClub.Web.Dtos;
using DowntownCarClub.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DowntownCarClub.Web.Controllers
{
    [Route("/questions")]
    public class QuestionController : Controller
    {
        private Guid CurrentUserId { get => this.GetCurrentUserId(); }
        private AppDbContext AppDbContext { get; }

        public QuestionController(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var userIsNotAuthenticated = !this.UserIsAuthenticated();
            if (userIsNotAuthenticated)
            {
                return RedirectToAction("SignIn", "Auth");
            }
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateQuestionDto model)
        {
            var question = model.ConvertToDomain(CurrentUserId);

            await AppDbContext.Questions.AddAsync(question);

            await AppDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { Slug = question.Slug });
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            var question = await AppDbContext.Questions
                .Include(x => x.User)
                .Include(x => x.Answers)
                .ThenInclude(x => x.User)
                .Select(x => new QuestionDetailsDto
                {
                    Id = x.Id,
                    Slug = x.Slug,
                    CreatedAt = x.CreatedAt,
                    Title = x.Title,
                    Content = x.Content,
                    UserName = x.User.UserName,
                    Answers = x.Answers.Select(y => new QuestionDetailsAnswerDto
                    {
                        Id = y.Id,
                        CreatedAt = y.CreatedAt,
                        UserName = y.User.UserName,
                        Content = y.Content,
                    }).ToList(),
                })
                .FirstOrDefaultAsync(x => x.Slug == slug);

            return View(question);
        }

    }
}
