using DowntownCarClub.Web.Context;
using DowntownCarClub.Web.Dtos;
using DowntownCarClub.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DowntownCarClub.Web.ViewComponents
{
    public class UserCardViewComponent : ViewComponent
    {
        private AppDbContext AppDbContext { get; set; }

        public UserCardViewComponent(AppDbContext context)
        {
            AppDbContext = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Guid? currentUserId;
            try
            {
                currentUserId = this.UserClaimsPrincipal.GetCurrentUserId();
            }
            catch
            {
                return View(new UserCardDto());
            }

            var user = await AppDbContext
                .Users
                .Include(x => x.Questions)
                .Include(x => x.Answers)
                .Select(x => new UserCardDto
                {
                    UserId = x.Id,
                    UserName = x.UserName,
                    Questions = x.Questions.Count(),
                    Answers = x.Answers.Count(),
                    UserIsLoggedIn = true,
                })
                .FirstOrDefaultAsync(x => x.UserId == currentUserId);

            if (user is null)
            {
                return View(new UserCardDto());
            }

            return View(user);
        }
    }
}
