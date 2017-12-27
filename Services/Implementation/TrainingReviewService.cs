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
        private ISimilarityAlgorithm similarityAlgorithm = new ShoterSimilarityAlgorithm();
        public TrainingReviewService(IFlashcardUnit unit, ISessionService sessionService)
        {
            this.unit = unit;
            this.sessionService = sessionService;
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
            return correctness;
        }

        public void DeclineAnswer(TrainingCard trainingFlashcard)
        {
            trainingFlashcard.InternalLossCount++;
            unit.SaveChanges();
        }

        public bool HasTrainingEnded(string userID, int languageID)
        {
            return unit.TrainingCardRepository.GetCardForTraining(userID, languageID) == null;
        }

        public TrainingCard GetTrainingCard(string userID, int languageID)
        {
            return unit.TrainingCardRepository.GetCardForTraining(userID, languageID);
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
        }

        public bool IsAnswerCorrect(FlashcardAnswer answer)
        {
            var correctess = CalculateCorrectnessOfAnswer(answer);

            if (correctess > 0.82f)
                return true;

            return false;
        }

        public virtual double CalculateCorrectnessOfAnswer(string correct, string answer)
        {
            return similarityAlgorithm.CalculateSimilarity(correct, answer);
        }

        public virtual double CalculateCorrectnessOfAnswer(FlashcardAnswer answer)
        {
            var translations = unit.FlashcardTranslationRepository.GetTRanslationsForFlashcard(answer.Flashcard.ID, answer.Language.ID);

            double maxScore = 0.0;
            foreach (var translation in translations)
            {
                var score = CalculateCorrectnessOfAnswer(translation.Translation, answer.Answer) * (double)translation.Significance;
                if (score > maxScore)
                    maxScore = score;
            }

            return maxScore;
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
