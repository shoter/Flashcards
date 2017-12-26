using Flashcards.Entities;
using Flashcards.Entities.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSuite.Dummies;

namespace UnitTests.ServicesTests
{
    [TestClass]
    public class FlashcardTranslationServiceTests
    {

        Mock<FlashcardTranslationService> mockFlashcardTranslationService;
        Mock<IFlashcardTranslationRepository> flashcardTranslationRepository = new Mock<IFlashcardTranslationRepository>();

        FlashcardTranslationService flashcardTranslationService => mockFlashcardTranslationService.Object;

        public FlashcardTranslationServiceTests()
        {
            mockFlashcardTranslationService = new Mock<FlashcardTranslationService>(flashcardTranslationRepository.Object);
            mockFlashcardTranslationService.CallBase = true;
        }


        [TestMethod]
        public void CanRemoveTranslationNullTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanRemoveTranslation(null).isSuccess);
        }

        [TestMethod]
        public void CanRemoveTranslationTest()
        {
            Assert.IsTrue(flashcardTranslationService.CanRemoveTranslation(
                getTranslation()
                ).isSuccess);
        }

        [TestMethod]
        public void RemoveTranslationTest()
        {
            var t = getTranslation();
            flashcardTranslationService.RemoveTranslation(t);

            flashcardTranslationRepository.Verify(x => x.Remove(It.Is<FlashcardTranslation>(
                dbT => dbT == t
                )), Times.Once);
        }

        [TestMethod]
        public void ChangeTranslationTest()
        {
            var t = getTranslation();
            

            var pron = "japko";
            var trans = "jabłko";
            var sig = 0.1;

            flashcardTranslationService.ChangeTranslation(t, trans, pron, sig);
            Assert.AreEqual(pron, t.Pronounciation);
            Assert.AreEqual(trans, t.Translation);
            Assert.AreEqual((decimal)sig, t.Significance);

            flashcardTranslationRepository.Verify(x => x.SaveChanges(), Times.Once);
        }



        [TestMethod]
        public void CanAddTranslationFlashcardNullTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanAddTranslation(null, getLang(), "test", "test", 1.0).isSuccess);
        }

        [TestMethod]
        public void CanAddTranslationLanguageNullest()
        {
            Assert.IsFalse(flashcardTranslationService.CanAddTranslation(getFlash(), null, "test", "test", 1.0).isSuccess);
        }


        [TestMethod]
        public void CanAddTranslationNegativeSignificanceTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanAddTranslation(getFlash(), getLang(), "test", "test", -1.0).isSuccess);
        }

        [TestMethod]
        public void CanAddTranslationBiggerThan1SignificanceTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanAddTranslation(getFlash(), getLang(), "test", "test", 1.2).isSuccess);
        }

        [TestMethod]
        public void CanAddTranslationEmptyTranslationTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanAddTranslation(getFlash(), getLang(), "", "test", 1.0).isSuccess);
        }

        [TestMethod]
        public void CanAddTranslationEmptyPronounceTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanAddTranslation(getFlash(), getLang(), "test", "", 1.0).isSuccess);
        }

        [TestMethod]
        public void CanAddTranslationTest()
        {
            Assert.IsTrue(flashcardTranslationService.CanAddTranslation(getFlash(), getLang(), "test", "test", 0.2).isSuccess);
        }


        [TestMethod]
        public void CanChangeTranslationNullTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanChangeTranslation(null, "test", "test", 1.0).isSuccess);
        }

        [TestMethod]
        public void CanChangeTranslationNegativeSignificanceTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanChangeTranslation(getTranslation(), "test", "test", -1.0).isSuccess);
        }

        [TestMethod]
        public void CanChangeTranslationBiggerThan1SignificanceTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanChangeTranslation(getTranslation(), "test", "test", 1.2).isSuccess);
        }

        [TestMethod]
        public void CanChangeTranslationEmptyTranslationTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanChangeTranslation(getTranslation(), "", "test", 1.0).isSuccess);
        }

        [TestMethod]
        public void CanChangeTranslationEmptyPronounceTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanChangeTranslation(getTranslation(), "test", "", 1.0).isSuccess);
        }

        [TestMethod]
        public void CanChangeTranslationTest()
        {
            Assert.IsTrue(flashcardTranslationService.CanChangeTranslation(getTranslation(), "test", "test", 0.2).isSuccess);
        }

        [TestMethod]
        public void AddTranslationTest()
        {
            var flash = getFlash();
            var lang = getLang();
            flashcardTranslationService.AddTranslation(flash, lang, "test", "prop", 0.5);

            flashcardTranslationRepository.Verify(x => x.Add(It.Is<FlashcardTranslation>(
                t => t.Translation == "test" && t.Pronounciation == "prop" && t.Significance == 0.5m && t.LanguageID == lang.ID
                )), Times.Once);

            flashcardTranslationRepository.Verify(x => x.SaveChanges(), Times.Once);
        }

        private FlashcardTranslation getTranslation()
        {
            return new FlashcardTranslationDummyCreator(new FlashcardDummyCreator().Create()).Create();
        }

        private Flashcard getFlash()
        {
            return new FlashcardDummyCreator().Create();
        }

        private Language getLang()
        {
            return new LanguageDummyCreator().Create();
        }
    }
}
