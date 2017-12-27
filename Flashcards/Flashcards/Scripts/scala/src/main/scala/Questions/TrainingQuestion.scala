package Flashcards.Questions
import Flashcards.Core.Url
import Flashcards.Http.HttpMethod
import Flashcards.Jquery.AjaxOptions
import Flashcards.Jquery.myquery.jQuery
import Flashcards.Json.JsonData
import Management.BasicSearchResult
import org.scalajs.dom
import dom.document
import org.scalajs.jquery.{JQueryAjaxSettings, JQueryEventObject, JQueryXHR}

import scalajs.js
import scala.scalajs.js.annotation.JSExportTopLevel
import js.Dynamic.{global => g}
import scala.scalajs.js.JSON

class TrainingQuestion {

  val answerInput = jQuery("input#questionAnswer")
  val question = jQuery("#question")
  val submit = jQuery("#question .answer button")
  var questionAnswer = jQuery("#questionAnswer")

  val flashcardID = jQuery("#FlashcardID").value()
  var trainingID = jQuery("#TrainingID").value()

  def Run(): Unit = {

    submit.click((JqueryEventObject) => onAnswer(JqueryEventObject))

  }

  def onAnswer(e: JQueryEventObject): Unit =
  {
    val answer = answerInput.value();
    val url = Url.CreateAddress("Answer", "Training")

    var data = js.Dynamic.literal(
      trainingID = trainingID,
      trainingCardID = flashcardID,
      answer = answer,
    )

    var settings = js.Dynamic.literal(
      url = url,
      data = data,
      success = onSearchSuccess _,
      `type` = "POST"
    ).asInstanceOf[JQueryAjaxSettings]

    g.console.log(answer)
    jQuery.ajax(settings)
  }

  def onSearchSuccess(data : js.Any): Unit = {
    g.console.log(data);
    var html = jQuery(data)
    question.after(html)
    submit.remove()
    questionAnswer.remove()
  }
}
