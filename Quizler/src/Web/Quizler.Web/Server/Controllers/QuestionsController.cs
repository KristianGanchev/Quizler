﻿namespace Quizler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Services.Data;
    using Quizler.Web.Shared.Models.Answers;
    using Quizler.Web.Shared.Models.Questions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class QuestionsController : ApiController
    {
        private readonly IQuestionsServices questionsServices;
        private readonly IAnswersServices answersServices;

        public QuestionsController(IQuestionsServices questionsServices, IAnswersServices answersServices)
        {
            this.questionsServices = questionsServices;
            this.answersServices = answersServices;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<QuestionResponse>> Create([FromBody] QuestionCreateRequest model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var questionId = await this.questionsServices.CreateAsync(model.Text, model.Points, model.QuizId);

            return new QuestionResponse { Text = model.Text, Id = questionId };
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<QuestionEditResponse> Edit(int id)
        {
            var quiz = this.questionsServices.GetByQuizId<QuestionEditResponse>(id);

            return quiz;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<QuestionResponse>> Update([FromBody] QuestionEditResponse model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var questionId = await this.questionsServices.UpdateAsync(model.Text, model.Points, model.Id);

            return new QuestionResponse { Text = model.Text, Id = questionId };
        }

        [HttpDelete("[action]/{id}")]
        public async Task<int> Delete([FromRoute] int id)
        {
            return await this.questionsServices.DeleteAsync(id);
        }

        [HttpGet("[action]/{quizId}")]
        public ActionResult<IEnumerable<QuestionResponse>> All(int quizId)
        {

            var questions = this.questionsServices.GetAll<QuestionResponse>(quizId).ToList();

            foreach (var question in questions)
            {
                var ansewers = this.answersServices.GetAll<AnswerResponse>(question.Id).ToList();

                question.Answers = ansewers;
            }

            return questions;
        }

        [HttpGet()]
        public ActionResult<int> GetId([FromQuery] string questionName, int quizId)
        {
            var questionId = this.questionsServices.GetId(questionName, quizId);

            return questionId;
        }
    }
}
