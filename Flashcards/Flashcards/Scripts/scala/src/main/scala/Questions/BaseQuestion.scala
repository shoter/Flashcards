package Flashcards.Questions

import Flashcards.Jquery.myquery.jQuery
import org.scalajs.jquery.{JQueryAjaxSettings, JQueryEventObject}

import scalajs.js
import scala.scalajs.js
import js.Dynamic.{global => g}


abstract class BaseQuestion {
  val answerInput = jQuery("input#questionAnswer")
  val question = jQuery("#question")
  val submit = jQuery("#question .answer button")
  var questionAnswer = jQuery("#questionAnswer")

  val flashcardID = jQuery("#FlashcardID").value()

  def Run(): Unit = {

    submit.click((JqueryEventObject) => onAnswer(JqueryEventObject))

  }

  def onAnswer(e: JQueryEventObject): Unit =
  {

    val url = getUrl
    var data = createData

    var settings = js.Dynamic.literal(
      url = url,
      data = data,
      success = onSearchSuccess _,
      `type` = "POST"
    ).asInstanceOf[JQueryAjaxSettings]
    jQuery.ajax(settings)
  }


  def onSearchSuccess(data : js.Any): Unit = {
    g.console.log(data);
    var html = jQuery(data)
    question.after(html)
    submit.remove()
    questionAnswer.remove()
  }

  def getUrl: String
  def createData: js.Dynamic
}
