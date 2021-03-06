﻿namespace Quizler.Web.Shared.Models.Quizzes
{
    using Quizler.Web.Shared.Attributes;
    using Quizler.Web.Shared.Models.Questions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class QuizCreateRequest
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Quiz name")]
        public string Name { get; set; }

        [Required]
        [CategoryRequiredValidation]
        [Display(Name = "Category")]
        public int CategorieId { get; set; }

        public string ImageUrl { get; set; }

        [QuetionLengthValidation]
        [Display(Name = "Questions")]
        public List<QuestionCreateRequest> Questions { get; set; }
    }
}
