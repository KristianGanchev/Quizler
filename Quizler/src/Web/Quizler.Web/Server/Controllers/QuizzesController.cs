﻿namespace Quizler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Data.Models;
    using Quizler.Services;
    using Quizler.Services.Data;
    using Quizler.Web.Shared.Models.Quizzes;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class QuizzesController : ApiController
    {
        private readonly IQuizzesService quizzesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICloudinaryService cloudinaryService;

        public QuizzesController(IQuizzesService quizzesService, UserManager<ApplicationUser> userManager, ICloudinaryService cloudinaryService)
        {
            this.quizzesService = quizzesService;
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<QuizResponse>> Create([FromBody] QuizCreateRequest model)
        {
            var userName = this.User.Identity.Name;

            var user = this.userManager.Users.SingleOrDefault(u => u.UserName == userName);

            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            string imageUrl = null;

            if (model.ImageUrl != null)
            {
               imageUrl = this.cloudinaryService.UploadPictureAsync(
               model.ImageUrl,
               model.Name);
            }

            var quizId = await this.quizzesService.CreateAsync(model.Name, model.CategorieId, user, imageUrl);

            return new QuizResponse { Id = quizId, Name = model.Name };
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<QuizEditResponse> Edit(int id)
        {
            var quiz = this.quizzesService.GetById<QuizEditResponse>(id);

            return quiz;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<QuizResponse>> Update([FromBody] QuizEditResponse model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string imageUrl = null;

            if (model.ImageUrl != null)
            {
                imageUrl = this.cloudinaryService.UploadPictureAsync(
                model.ImageUrl,
                model.Name);
            }

            var quizId = await this.quizzesService.UpdateAsync(model.Name, model.CategorieId, imageUrl, model.Id);

            return new QuizResponse { Id = quizId, Name = model.Name};
        }

        [HttpDelete("[action]/{id}")]
        public async Task<int> Delete([FromRoute] int id)
        {
           return await this.quizzesService.DeleteAsync(id);
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<QuizDetailsResponse> Details(int id)
        {
            var quiz = this.quizzesService.GetById<QuizDetailsResponse>(id);

            return quiz;
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<QuizPlayResponse> Play(int id)
        {
            var quiz = this.quizzesService.GetById<QuizPlayResponse>(id);

            return quiz;
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<QuizAllResponse>> MyQuizzes()
        {
            var userName = this.User.Identity.Name;

            var user = this.userManager.Users.SingleOrDefault(u => u.UserName == userName);

            var myQuizzes = this.quizzesService.GetAllByUser<QuizAllResponse>(user.Id);

            return myQuizzes.ToList();
        }

        [HttpGet("[action]/{searchQuery}")]
        public ActionResult<IEnumerable<QuizAllResponse>> Search(string searchQuery)
        {
            var myQuizzes = this.quizzesService.Search<QuizAllResponse>(searchQuery);

            return myQuizzes.ToList();
        }
    }
}
