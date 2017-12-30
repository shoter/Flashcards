package Flashcards.Core


import Flashcards.Questions
import scala.scalajs.js.annotation.JSExportTopLevel
import org.scalajs.dom
import dom.document
import org.scalajs.jquery.{JQueryEventObject, jQuery}

object Main {
  val languageSelect = jQuery("#currentLanguageID")
  val languageSelectForm = jQuery("#languageChangeForm")
def main(args: Array[String]): Unit = {
  languageSelect.change(() => changeLanguage())
  }

  def changeLanguage(): Unit = {
    languageSelectForm.submit()
  }

  @JSExportTopLevel("InitFlashcard")
  def Init(moduleName: String): Unit =
  {
    moduleName match
      {
      case "Management.Index" => new  Flashcards.Management.Index().Run()
      case "Management.EditFlashcard" => new  Flashcards.Management.EditFlashcard().Run()
      case "Training.Question" => new Flashcards.Questions.TrainingQuestion().Run()
      case "Review.Question" => new Flashcards.Questions.ReviewQuestion().Run()
    }

  }
}
