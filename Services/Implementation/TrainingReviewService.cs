using Common.Words.Similiraties;
using Flashcards.Entities;
using Services.Interfaces;
using Services.Models;
using Services.Session.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class TrainingReviewService : ITrainingReviewService
    {
        private readonly IFlashcardUnit unit;
        private readonly ISessionService sessionService;
        private readonly IQuestionService questionService;
        
        public TrainingReviewService(IFlashcardUnit unit, ISessionService sessionService, IQuestionService questionService)
        {
            this.unit = unit;
            this.sessionService = sessionService;
            this.questionService = questionService;
        }

        public List<Flashcard> GetTrainingFlashcards(int languageID, string userID)
        {
            return unit.TrainingCardRepository.GetTrainableFlaschards(languageID, userID, limit: 5);
        }

        public double AcceptAnswer(TrainingCard trainingFlashcard, FlashcardAnswer answer)
        {
            var correctness = CalculateCorrectnessOfAnswer(answer);
            unit.UserFlashcardMemoryRepository.AddBasedOnTraining(trainingFlashcard, (decimal)(correctness* correctness));
            unit.TrainingCardRepository.Remove(trainingFlashcard);
            unit.SaveChanges();

            sessionService.UserInfo.TrainingInfo.Cards.RemoveAt(0);
            return correctness;
        }

        public void DeclineAnswer(TrainingCard trainingFlashcard)
        {
            trainingFlashcard.InternalLossCount++;

            var first = sessionService.UserInfo.TrainingInfo.Cards[0];
            first.InternalLossCount++;
            sessionService.UserInfo.TrainingInfo.Cards.RemoveAt(0);
            sessionService.UserInfo.TrainingInfo.Cards.Add(first);

          unit.SaveChanges();
        }

        public virtual bool HasTrainingEnded(string userID, int languageID)
        {
            return unit.TrainingCardRepository.GetCardForTraining(userID, languageID) == null;
        }

        public TrainingCard GetTrainingCard(string userID, int languageID)
        {
            if ((sessionService.UserInfo?.TrainingInfo?.Cards?.Count ?? 0) == 0)
                return null;

            var card = unit.TrainingCardRepository.GetCardForTraining(sessionService.UserInfo.TrainingInfo.TrainingID, sessionService.UserInfo.TrainingInfo.Cards[0].FlashcardID);
            //we could start review in the same request. We created this entity using only ID of the flashcard and there is no information on entity framework side about flashcard.
            if (card.Flashcard == null && card.FlashcardID > 0)
                card.Flashcard = unit.FlashcardRepository.GetById(card.FlashcardID);
            return card;
        }

        public void TryToAddNewCards()
        {
            if (sessionService.UserInfo.TrainingInfo == null)
                return;
            while (AddNewReviewCardIfAble()) ;

        }

        public bool AddNewReviewCardIfAble()
        {
            if (sessionService.UserInfo.TrainingInfo.Cards.Count >= 5)
                return false;

            var card = unit.TrainingRepository.GetTrainableFlashcards(sessionService.UserID, sessionService.LanguageID, 1).FirstOrDefault();

            if (card == null)
                return false;

            var trainingCard = new TrainingCard()
            {
                FlashcardID = card.ID,
                TrainingID = sessionService.UserInfo.TrainingInfo.TrainingID
            };
            unit.TrainingCardRepository.Add(trainingCard);
            unit.SaveChanges();
            sessionService.UserInfo.TrainingInfo.AddCard(trainingCard, sessionService.LanguageID);
            return true;
        }


        public void StartTraining(string userID, int languageID)
        {
            var cards = unit.TrainingRepository
                .GetTrainableFlashcards(userID, languageID, count: 5);
            if (cards.Count == 0)
                return;

            var training = new TrainingSession()
            {
                DateStarted = DateTime.Now,
                LanguageID = languageID,
                UserID = userID
            };

            foreach (var card in cards)
                training.TrainingCards.Add(new TrainingCard()
                {
                    FlashcardID = card.ID,
                    InternalLossCount = 0
                });

            unit.TrainingRepository.Add(training);
            unit.TrainingRepository.SaveChanges();

            sessionService.UserInfo.TrainingInfo = new TrainingInfo(training);
        }

        public bool IsAnswerCorrect(FlashcardAnswer answer)
        {
            var correctess = CalculateCorrectnessOfAnswer(answer);

            if (correctess > 0.82f)
                return true;

            return false;
        }

        

        public virtual double CalculateCorrectnessOfAnswer(FlashcardAnswer answer)
        {
            return questionService.CalculateCorrectnessOfAnswer(answer);
        }

        public bool ShouldStopLastTraining()
        {
            if (sessionService.UserInfo.TrainingInfo == null)
                return false;

            var trExpDate = CalculateTrainingExpirationDate();

            if (DateTime.Now > trExpDate)
                return true;

            if (HasTrainingEnded(sessionService.UserID, sessionService.LanguageID))
                return true;

            return false;
        }

        public void StopLastTraining()
        {
            long trainingID = sessionService.UserInfo.TrainingInfo.TrainingID;
            unit.TrainingRepository.Remove(trainingID);

            sessionService.UserInfo.TrainingInfo = null;

            unit.SaveChanges();

        }

        public virtual DateTime CalculateTrainingExpirationDate()
        {
            if (sessionService.UserInfo.TrainingInfo == null)
                throw new NullReferenceException();
            return sessionService.UserInfo.TrainingInfo.DateStarted.AddMinutes(30);
        }
    }
}
