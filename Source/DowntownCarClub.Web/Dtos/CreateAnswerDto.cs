using DowntownCarClub.Web.Models;
using System;

namespace DowntownCarClub.Web.Dtos
{
    public class CreateAnswerDto
    {
        public string Content { get; set; }

        public Answer ConvertToDomain(Guid userId, Guid questionId)
        {
            return new Answer
            {
                UserId = userId,
                QuestionId = questionId,
                Content = Content,
            };
        }
    }
}
