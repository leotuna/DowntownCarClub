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
    [Route("/questions/{slug}/answers")]
    public class AnswerController : Controller
    {
        private Guid CurrentUserId { get => this.GetCurrentUserId(); }
        private AppDbContext AppDbContext { get; }

        public AnswerController(
            AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string slug, CreateAnswerDto model)
        {
            var question = await AppDbContext.Questions.Select(x => new { x.Id, x.Slug }).FirstOrDefaultAsync(x => x.Slug == slug);
            if (question is null)
            {
                throw new NotImplementedException();
            }

            var answer = model.ConvertToDomain(CurrentUserId, question.Id);

            await AppDbContext.Answers.AddAsync(answer);

            await AppDbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Question", new { Slug = slug });
        }
    }
}
