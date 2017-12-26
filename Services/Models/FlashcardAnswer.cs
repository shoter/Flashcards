using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class FlashcardAnswer
    {
        public Flashcard Flashcard { get; private set; }
        public string Answer { get; private set; }
        public Language Language { get; private set; }

        public FlashcardAnswer(Flashcard flashcard, Language language, string answer)
        {
            Flashcard = flashcard;
            Answer = answer;
            Language = language;
        }
        
    }
}
