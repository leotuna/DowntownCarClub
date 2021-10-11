using System;

namespace DowntownCarClub.Web.Dtos
{
    public class UserCardDto
    {
        public bool UserIsLoggedIn { get; set; } = false;
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public int? Questions { get; set; }
        public int? Answers { get; set; }
    }
}
