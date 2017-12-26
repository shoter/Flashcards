using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Models
{
    public class TrainingCardInfo
    {
        public int FlashcardID { get; set; }
        public int LanguageID { get; set; }
        public int InternalLossCount { get; set; }
    }
}
