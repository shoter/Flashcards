using Common.EntityFramework;
using Flashcards.Entities.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Flashcards.Entities.Repositories
{
    public class InfoRepository : RepositoryBaseEntityless<FlashcardsEntities>, IInfoRepository
    {
        public InfoRepository(FlashcardsEntities context) : base(context)
        {
        }

        public UserInfo GetUserInfo(string userID, int languageID)
        {

            return (from user in context.AspNetUsers.Where(user => user.Id == userID)

                    join training in context.TrainingSessions.Where(t => t.LanguageID == languageID) on user.Id equals training.UserID into trainings
                    from training in trainings.DefaultIfEmpty()
                    join trainingCard in context.TrainingCards on training.ID equals trainingCard.TrainingID into trainingCards

                    join review in context.InternalReviews.Where(r => r.LanguageID == languageID) on user.Id equals review.UserID into reviews
                    from review in reviews.DefaultIfEmpty()
                    join reviewCard in context.ReviewCards on review.ID equals reviewCard.ReviewID into reviewCards

                    select new UserInfo()
                    {
                        UserID = userID,
                        Username = user.UserName,
                        TrainingID = training == null ? (long?)null : training.ID,
                        TrainingDateStarted = training == null ? (DateTime?)null : training.DateStarted,
                        ReviewID = review == null ? (long?)null : review.ID,
                        ReviewDateStarted = review == null ? (DateTime?)null : review.DateStarted,
                        ReviewLanguageID = review == null ? (int?)null : review.LanguageID,
                        TrainingCards = trainingCards
                        .Select(card => new TrainingCardInfo()
                        {
                            FlashcardID = card.FlashcardID,
                            InternalLossCount = card.InternalLossCount,
                            LanguageID = card.TrainingSession.LanguageID
                        }),
                        ReviewCards = reviewCards
                        .Select(card => new DbReviewCard()
                        {
                            FlashcardID = card.FlashcardID,
                            InternalLossCount = card.InternallLossCount
                        })
                    }).FirstOrDefault();
        }
    }
}
