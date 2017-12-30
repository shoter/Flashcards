using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services.Interfaces;
using System.Web.Mvc;
using Flashcards.Models.Flashcards;
using Flashcards.Entities;
using Flashcards.Models.Reviews;
using Services.Models;

namespace Flashcards.Controllers
{
    public class ReviewController : ControllerBase
    {
        private readonly IInternalReviewService reviewService;
        private readonly IFlashcardUnit unit;
        public ReviewController(IPopupService popupService, ISessionService sessionService, IInternalReviewService reviewService,
            IFlashcardUnit unit) : base(popupService, sessionService)
        {
            this.reviewService = reviewService;
            this.unit = unit;
        }

        public ActionResult Review()
        {
            if (reviewService.ShouldStopLastReview())
                reviewService.StopLastReview();

            if (reviewService.CanStartReview())
                reviewService.StartReview();

            reviewService.TryToAddNewCards();

            ReviewQuestionViewModel vm = null;
            if (reviewService.HasReviewEnded() == false)
            {
                var card = reviewService.GetReviewCard();
                vm = new ReviewQuestionViewModel(card, sessionService.LanguageID);
            }

            return View(vm);
        }


        public ActionResult Answer(long reviewID, int flashcardID, string answer)
        {
            var review = unit.InternalReviewRepository.GetById(reviewID);
            var card = unit.ReviewCardRepository.GetCardForReview(reviewID, flashcardID);

            if (card == null || review == null)
                return View("QuestionError");


            var ans = new FlashcardAnswer(card.Flashcard, review.Language, answer);

            bool isCorrect = false;
            double correctness = 0.0;
            if ((isCorrect = reviewService.IsAnswerCorrect(ans)) == true)
            {
                correctness = reviewService.AcceptAnswer(card, ans);

            }
            else
            {
                reviewService.DeclineAnswer(card);
            }

            var vm = new AnswerViewModel(flashcardID, review.Language, isCorrect, correctness);

            return PartialView(vm);
        }
    }
}