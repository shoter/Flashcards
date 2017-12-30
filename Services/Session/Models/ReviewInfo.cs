using Flashcards.Entities;
using Flashcards.Entities.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Session.Models
{
    public class ReviewInfo
    {
        public long ReviewID { get; set; }
        public int LanguageID { get; set; }
        public DateTime DateStarted { get; set; }

        public List<ReviewCardInfo> ReviewCards { get; set; }

        public ReviewInfo(Flashcards.Entities.Models.UserInfo userInfo)
        {
            ReviewID = userInfo.ReviewID.Value;
            LanguageID = userInfo.ReviewLanguageID.Value;
            DateStarted = userInfo.ReviewDateStarted.Value;

            ReviewCards = userInfo.ReviewCards
                .Select(card => new ReviewCardInfo(card))
                .OrderByDescending(card => card.InternalLossCount)
                .ToList();
        }

        public ReviewInfo(InternalReview review)
        {
            ReviewID = review.ID;
            LanguageID = review.LanguageID;
            DateStarted = review.DateStarted;

            ReviewCards = review.ReviewCards.ToList()
                .Select(card => new ReviewCardInfo(card))
                .OrderByDescending(card => card.InternalLossCount)
                .ToList();
        }

        public void AddCard(DbReviewCard card)
        {
            ReviewCards.Add(new ReviewCardInfo(card));
        }
    }
}
