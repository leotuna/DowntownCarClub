using DowntownCarClub.Web.Models;
using System;
using System.Collections.Generic;

namespace DowntownCarClub.Web.Dtos
{
    public class QuestionDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string UserName { get; set; }
        public int Answers { get; set; }

        public static List<QuestionDto> ConvertDomainToDto(List<Question> domain)
        {
            var model = new List<QuestionDto>();

            foreach (var item in domain)
            {
                if (item.User is null)
                {
                    throw new Exception();
                }
                if (item.Answers is null)
                {
                    throw new Exception();
                }
                model.Add(new QuestionDto
                {
                    Title = item.Title,
                    Slug = item.Slug,
                    UserName = item.User.UserName,
                    Answers = item.Answers.Count,
                });
            }

            return model;
        }
    }
}
