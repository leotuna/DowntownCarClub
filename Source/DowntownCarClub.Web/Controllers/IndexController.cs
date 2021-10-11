using DowntownCarClub.Web.Context;
using DowntownCarClub.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DowntownCarClub.Web.Controllers
{
    [Route("/")]
    public class IndexController : Controller
    {
        private AppDbContext AppDbContext { get; }

        public IndexController(
            AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var questions = await AppDbContext
                .Questions
                .OrderBy(x => x.CreatedAt)
                .Include(x => x.User)
                .Include(x => x.Answers)
                .ToListAsync();

            return View(new IndexPageDto
            {
                Questions = QuestionDto.ConvertDomainToDto(questions),
            });
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return View();
        }
    }
}
