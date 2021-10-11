using System;
using System.Collections.Generic;

namespace DowntownCarClub.Web.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public bool UserCanLogOut { get; set; }
        public string UserName { get; set; }
        public int QuestionQuantity { get => Questions.Count; }
        public int AnswerQuantity { get => Answers.Count; }
        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
        public List<QuestionDto> Answers { get; set; } = new List<QuestionDto>();
    }
}
