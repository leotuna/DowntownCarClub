using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DowntownCarClub.Web.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;

        public List<Question> Questions { get; set; }

        public List<Answer> Answers { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
