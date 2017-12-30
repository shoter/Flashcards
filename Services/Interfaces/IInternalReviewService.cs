using Flashcards.Entities;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IInternalReviewService
    {
        double AcceptAnswer(ReviewCard card, FlashcardAnswer answer);
        void DeclineAnswer(ReviewCard reviewCard);


        bool ShouldStopLastReview();
        void StopLastReview();

        bool CanStartReview();
        void StartReview();
        ReviewCard GetReviewCard();
        bool HasReviewEnded();
        bool IsAnswerCorrect(FlashcardAnswer answer);
        void TryToAddNewCards();
    }
}
