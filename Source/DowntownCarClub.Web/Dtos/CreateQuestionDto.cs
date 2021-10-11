using DowntownCarClub.Web.Models;
using System;

namespace DowntownCarClub.Web.Dtos
{
    public class CreateQuestionDto
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Question ConvertToDomain(Guid userId)
        {
            return new Question
            {
                UserId = userId,
                Title = Title,
                Content = Content,
            };
        }
    }
}
