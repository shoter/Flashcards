using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models;
using Flashcards.Entities;
using Common.Words.Similiraties;

namespace Services.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly IFlashcardUnit unit;
        private ISimilarityAlgorithm similarityAlgorithm = new LevenshteinSimilarity();

        public QuestionService(IFlashcardUnit unit)
        {
            this.unit = unit;
        }
        public double CalculateCorrectnessOfAnswerWithoutSignificance(FlashcardAnswer answer)
        {
            var translations = unit.FlashcardTranslationRepository.GetTRanslationsForFlashcard(answer.Flashcard.ID, answer.Language.ID);

            double maxScore = 0.0;
            foreach (var translation in translations)
            {
                var score = CalculateCorrectnessOfAnswer(translation.Translation, answer.Answer);
                if (score > maxScore)
                    maxScore = score;
            }

            return maxScore;
        }

        public double CalculateCorrectnessOfAnswer(FlashcardAnswer answer)
        {
            var translations = unit.FlashcardTranslationRepository.GetTRanslationsForFlashcard(answer.Flashcard.ID, answer.Language.ID);

            double maxScore = 0.0;
            foreach (var translation in translations)
            {
                var score = CalculateCorrectnessOfAnswer(translation.Translation, answer.Answer) * (double)translation.Significance;
                if (score > maxScore)
                    maxScore = score;
            }

            return maxScore;
        }

        public virtual double CalculateCorrectnessOfAnswer(string correct, string answer)
        {
            return similarityAlgorithm.CalculateSimilarity(correct, answer);
        }
    }
}
