﻿namespace Quizler.Web.Shared.Models.Quizzes
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Models.Questions;
    using System.Collections.Generic;

    public class QuizResultDetails : IMapFrom<Quiz>
    {
        public string Name { get; set; }

        public List<QuestionsResultDetails> Questions { get; set; }
    }
}
