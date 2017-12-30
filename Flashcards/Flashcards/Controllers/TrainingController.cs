using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Interfaces;
using Flashcards.Models.Flashcards;
using Flashcards.Entities;
using Flashcards.Models.Trainings;
using Services.Models;

namespace Flashcards.Controllers
{
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingReviewService trainingReviewService;
        private readonly IFlashcardUnit unit;
        public TrainingController(IPopupService popupService, ITrainingReviewService trainingReviewService, ISessionService sessionService, IFlashcardUnit unit) : base(popupService, sessionService)
        {
            this.trainingReviewService = trainingReviewService;
            this.unit = unit;
        }


        public ActionResult Training()
        {
            if (trainingReviewService.ShouldStopLastTraining())
            {
                trainingReviewService.StopLastTraining();

            }

            if (trainingReviewService.HasTrainingEnded(sessionService.UserID, sessionService.LanguageID))
                trainingReviewService.StartTraining(sessionService.UserID, sessionService.LanguageID);

            return RedirectToAction(nameof(Question));
        }

        public ActionResult Question()
        {
            TrainingQuestion vm = null;

            if (trainingReviewService.HasTrainingEnded(sessionService.UserID, sessionService.LanguageID) == false)
            {
                var card = trainingReviewService.GetTrainingCard(sessionService.UserID, sessionService.LanguageID);
                vm = new TrainingQuestion(card, sessionService.LanguageID);
            }

            return View(vm);
        }

        public ActionResult Answer(int trainingID, long trainingCardID, string answer)
        {
            var training = unit.TrainingRepository.GetById(trainingID);
            var card = training.TrainingCards.First(c => c.FlashcardID == trainingCardID);

            var ans = new FlashcardAnswer(card.Flashcard, training.Language, answer);

            bool isCorrect = false;
            double correctness = 0.0;
            int flashcardID = card.FlashcardID;
            if ((isCorrect = trainingReviewService.IsAnswerCorrect(ans)) == true)
            {
                correctness = trainingReviewService.AcceptAnswer(card, ans);
                
            }
            else
            {
                trainingReviewService.DeclineAnswer(card);
            }

            var vm = new AnswerViewModel(flashcardID, training.Language, isCorrect, correctness);

            return PartialView(vm);            
        }
    }
}