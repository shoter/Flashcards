using Common.Extensions;
using Flashcards.Entities;
using Flashcards.Entities.Models;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class InternalReviewService : IInternalReviewService
    {
        private readonly ISessionService sessionService;
        private readonly IFlashcardUnit unit;
        private readonly IUserFLashcardMemoryService userFLashcardMemoryService;
        private readonly IQuestionService questionService;
        private Random rand = new Random();
        public InternalReviewService(IFlashcardUnit unit, ISessionService sessionService, IUserFLashcardMemoryService userFLashcardMemoryService,
            IQuestionService questionService)
        {
            this.unit = unit;
            this.sessionService = sessionService;
            this.userFLashcardMemoryService = userFLashcardMemoryService;
            this.questionService = questionService;
        }
        public double AcceptAnswer(ReviewCard card, FlashcardAnswer answer)
        {
            var correctness = userFLashcardMemoryService.UpdateMemoryBasedOnAnswer(card, answer);
            RemoveReviewCard(card);
            AddNewReviewCardIfAble();
            return correctness;
        }

        public void RemoveReviewCard(ReviewCard card)
        {
            sessionService.UserInfo.ReviewInfo.ReviewCards.Remove(c => c.FlashcardID == card.FlashcardID);
            unit.ReviewCardRepository.Remove(c => c.ReviewID == card.ReviewID && c.FlashcardID == card.FlashcardID);
        }

        public void TryToAddNewCards()
        {
            if (sessionService.UserInfo.ReviewInfo == null)
                return;
            while (AddNewReviewCardIfAble()) ;
            
        }

        public bool AddNewReviewCardIfAble()
        {
            if (sessionService.UserInfo.ReviewInfo.ReviewCards.Count >= 30)
                return false;

            var memory = unit.UserFlashcardMemoryRepository.GetFirstTrainableMemories(sessionService.UserID, sessionService.LanguageID, 1).FirstOrDefault();

            if (memory == null)
                return false;

            var reviewCard = unit.ReviewCardRepository.AddCardBasedOnMemory(memory, sessionService.UserInfo.ReviewInfo.ReviewID);
            unit.SaveChanges();
            sessionService.UserInfo.ReviewInfo.AddCard(new DbReviewCard(reviewCard));
            return true;
        }

        public bool ShouldStopLastReview()
        {
            if (sessionService.UserInfo.ReviewInfo == null)
                return false;

            DateTime trExpDate = CalculateReviewExpirationDate();

            if (DateTime.Now > trExpDate)
                return true;

            if (HasReviewEnded())
                return true;

            return false;
        }

        public bool CanStartReview()
        {
            if (unit.UserFlashcardMemoryRepository.GetFirstTrainableMemories(sessionService.UserID, sessionService.LanguageID, 1).FirstOrDefault() == null)
                return false;
            if (sessionService.UserInfo.ReviewInfo != null)
                return false;

            return true;
        }

        public void StopLastReview()
        {
            long reviewID = sessionService.UserInfo.ReviewInfo.ReviewID;

            unit.InternalReviewRepository.Remove(reviewID);
            sessionService.UserInfo.ReviewInfo = null;
            unit.SaveChanges();

        }

        public void StartReview()
        {
            var memories = unit.UserFlashcardMemoryRepository.GetFirstTrainableMemories(sessionService.UserID, sessionService.LanguageID, 30);

            var review = new InternalReview()
            {
                DateStarted = DateTime.Now,
                LanguageID = sessionService.LanguageID,
                UserID = sessionService.UserID,
            };

            foreach (var memory in memories)
                review.ReviewCards.Add(new ReviewCard()
                {
                    FlashcardID = memory.FlashcardID,
                    InternallLossCount = 0
                });

            unit.InternalReviewRepository.Add(review);
            unit.SaveChanges();
            sessionService.UserInfo.ReviewInfo = new Session.Models.ReviewInfo(review);
        }

        public ReviewCard GetReviewCard()
        {
            if ((sessionService.UserInfo?.ReviewInfo?.ReviewCards?.Count ?? 0) == 0)
                return null;

            var card = unit.ReviewCardRepository.GetCardForReview(sessionService.UserInfo.ReviewInfo.ReviewID, sessionService.UserInfo.ReviewInfo.ReviewCards[0].FlashcardID);
            //we could start review in the same request. We created this entity using only ID of the flashcard and there is no information on entity framework side about flashcard.
            if (card.Flashcard == null && card.FlashcardID > 0)
                card.Flashcard = unit.FlashcardRepository.GetById(card.FlashcardID);
            return card;
        }

        public bool HasReviewEnded()
        {
            return GetReviewCard() == null;
        }

        public DateTime CalculateReviewExpirationDate()
        {
            if (sessionService.UserInfo.ReviewInfo == null)
                throw new NullReferenceException();
            return sessionService.UserInfo.ReviewInfo.DateStarted.AddMinutes(60);
        }

        public void DeclineAnswer(ReviewCard reviewCard)
        {
            reviewCard.InternallLossCount++;
            MoveDeclinedFlashcardInQueue(reviewCard.Flashcard.ID);
            unit.ReviewCardRepository.UpdateCardLossCount(reviewCard.ReviewID, reviewCard.FlashcardID, reviewCard.InternallLossCount);
        }

        public void MoveDeclinedFlashcardInQueue(int flashcardID)
        {
            var revInfo = sessionService.UserInfo.ReviewInfo;
            //take card
            var card = revInfo.ReviewCards.TakeFirst(c => c.FlashcardID == flashcardID);
            //put it in other place in queue
            int cardCount = revInfo.ReviewCards.Count;
            int place = GetPlacementForCardInQueue(cardCount, card.InternalLossCount);
            revInfo.ReviewCards.Insert(place, card);
        }


        public int GetPlacementForCardInQueue(int cardCount, int lossCount)
        {
            int factor = Math.Min(lossCount, 3);
            int min = cardCount / (factor + 2);
            int max = cardCount / (factor + 1);

            return rand.Next(min, max);
        }

        public bool IsAnswerCorrect(FlashcardAnswer answer)
        {
            var correctess = questionService.CalculateCorrectnessOfAnswerWithoutSignificance(answer);

            if (correctess >= 0.7f)
                return true;

            return false;
        }
    }
}
