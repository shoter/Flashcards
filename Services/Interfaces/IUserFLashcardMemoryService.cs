using Flashcards.Entities;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserFLashcardMemoryService
    {
        /// <returns>Correctness</returns>
        double UpdateMemoryBasedOnAnswer(ReviewCard card, FlashcardAnswer answer);
    }
}
