using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Management.ViewModels
{
    public class TranslationViewModel
    {
        public long? ID { get; set; }
        public BitmapImage TranslationImage { get; set; } = Resources.Images.Placeholder;
        public string Translation { get; set; }
        public string Pronounciation { get; set; }
        public double Significance { get; set; } = 1.0;

        public TranslationViewModel() { }

        public TranslationViewModel(FlashcardTranslation translation)
        {
            ID = translation.ID;
            Translation = translation.Translation;
            Pronounciation = translation.Pronounciation;
            Significance = (double)translation.Significance;
        }
    }
}
