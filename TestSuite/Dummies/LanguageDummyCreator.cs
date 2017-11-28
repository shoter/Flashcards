using Common.utilities;
using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite.Dummies
{
    public class LanguageDummyCreator : IDummyCreator<Language>
    {
        private static UniqueIDGenerator uniqueID = new UniqueIDGenerator();
        private Language language;
        public LanguageDummyCreator()
        {
            language = create();
        }
        private Language create()
        {
            var name = RandomGenerator.GenerateString(10);
            return new Language()
            {
                ID = uniqueID,
                EnglishName = name,
                Symbol = name.Substring(0, 2)
            };
        }
        public Language Create()
        {
            var temp = language;
            language = create();
            return temp;
        }
    }
}
