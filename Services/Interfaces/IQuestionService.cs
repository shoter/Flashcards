using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IQuestionService
    {
        double CalculateCorrectnessOfAnswer(FlashcardAnswer answer);
        double CalculateCorrectnessOfAnswer(string correct, string answer);
        double CalculateCorrectnessOfAnswerWithoutSignificance(FlashcardAnswer answer);
    }
}
