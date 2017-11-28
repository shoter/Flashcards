using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Services
{
    [TestClass]
    public class FlashcardTranslationServiceTests
    {

        Mock<FlashcardTranslationService> mockFlashcardTranslationService;
        FlashcardTranslationService flashcardTranslationService => mockFlashcardTranslationService.Object;

        public FlashcardTranslationServiceTests()
        {
            mockFlashcardTranslationService = new Mock<FlashcardTranslationService>();
            mockFlashcardTranslationService.CallBase = true;
        }


        [TestMethod]
        public void CanRemoveTranslationNullTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanRemoveTranslation(null).isSuccess);
        }

        [TestMethod]
        public void CanRemoveTranslationlTest()
        {
            Assert.IsFalse(flashcardTranslationService.CanRemoveTranslation(null).isSuccess);
        }
    }
}
