package Flashcards.Management

import Flashcards.Jquery.myquery.jQuery
import org.scalajs.jquery.{JQuery, JQueryEventObject}
import Flashcards.Core.StringUtils.IsEmpty

import scala.scalajs.js
import js.Dynamic.{global => g}

class EditFlashcard {
  val changeLanguageForm = jQuery("form#changeLanguage")
  val languageSymbol = jQuery("#LanguageSymbol")
  def Run(): Unit =
  {
    languageSymbol.change(onLanguageChange _)

  }

  def onLanguageChange(arg: JQueryEventObject): Unit = {
    val value = languageSymbol.value().asInstanceOf[String];
    if(IsEmpty(value) == false){
      changeLanguageForm.submit()
    }

  }

}
