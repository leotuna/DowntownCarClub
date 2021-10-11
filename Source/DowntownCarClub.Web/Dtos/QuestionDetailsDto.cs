using System;
using System.Collections.Generic;

namespace DowntownCarClub.Web.Dtos
{
    public class QuestionDetailsDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public int AnswerQuantity { get => Answers.Count; }
        public List<QuestionDetailsAnswerDto> Answers { get; set; } = new List<QuestionDetailsAnswerDto>();
        public string Answer { get; set; }
    }

    public class QuestionDetailsAnswerDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
    }
}
