using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DowntownCarClub.Web.Models
{
    public class Answer : Entity
    {
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public Guid QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }

        public string Content { get; set; }
    }
}
