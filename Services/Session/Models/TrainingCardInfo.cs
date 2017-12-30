using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Session.Models
{
    public class TrainingCardInfo
    {
        public int FlashcardID { get; set; }
        public int LanguageID { get; set; }
        public int InternalLossCount { get; set; }

        public TrainingCardInfo(Flashcards.Entities.Models.TrainingCardInfo cardInfo)
        {
            FlashcardID = cardInfo.FlashcardID;
            LanguageID = cardInfo.LanguageID;
            InternalLossCount = cardInfo.InternalLossCount;
        }

        public TrainingCardInfo(TrainingCard card)
        {
            FlashcardID = card.FlashcardID;
            LanguageID = card.TrainingSession.LanguageID;
            InternalLossCount = card.InternalLossCount;
        }

    }
}
