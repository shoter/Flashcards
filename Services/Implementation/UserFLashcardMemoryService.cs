using Flashcards.Entities;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class UserFLashcardMemoryService : IUserFLashcardMemoryService
    {
        private readonly IFlashcardUnit unit;
        private readonly ISessionService sessionService;
        private readonly IQuestionService questionService;
        private readonly IStrengthService strengthService;
        private readonly IIntervalService intervalService;

        public UserFLashcardMemoryService(IFlashcardUnit unit, ISessionService sessionService, IQuestionService questionService, IStrengthService strengthService, 
            IIntervalService intervalService)
        {
            this.unit = unit;
            this.sessionService = sessionService;
            this.questionService = questionService;
            this.strengthService = strengthService;
            this.intervalService = intervalService;
        }

        public double UpdateMemoryBasedOnAnswer(ReviewCard card, FlashcardAnswer answer)
        {
            var memory = unit.UserFlashcardMemoryRepository
                .GetMemoryBasedOnReview(card.ReviewID, card.FlashcardID, sessionService.UserID);

            var correctness = questionService.CalculateCorrectnessOfAnswer(answer);

            double newStrength = calculateNewStrength(card, memory, correctness);
            int newInterval = calculateNewInterval(memory, newStrength, card);

            memory.Strength = (decimal)newStrength;
            memory.LastInterval = newInterval;
            memory.IntervalCount++;
            memory.ReviewDate = DateTime.Now.AddMinutes(newInterval);

            unit.SaveChanges();
            return correctness;
        }

        private int calculateNewInterval(UserFlashcardMemory memory, double newStrength, ReviewCard card)
        {
            if (card.InternallLossCount == 0)
                return intervalService.CalculateNewIntervalForWin(memory.LastInterval, newStrength, memory.IntervalCount + 1);
            else
                return intervalService.CalculateNewIntervalForLoss(memory.LastInterval, newStrength, memory.IntervalCount + 1);
        }

        private double calculateNewStrength(ReviewCard card, UserFlashcardMemory oldMemory, double correctness)
        {
            if (card.InternallLossCount == 0)
                return strengthService.CalculateStrengthAfterGoodAnswer((double)oldMemory.Strength, correctness);
            else
                return strengthService.CalculateStrengthAfterWinningBadAnswer((double)oldMemory.Strength, card.InternallLossCount);
        }
    }
}
