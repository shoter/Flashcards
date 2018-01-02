package Flashcards.Questions

import Flashcards.Jquery.myquery.jQuery
import Jquery.JqOffset
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
    questionAnswer.keyup((eventObject: JQueryEventObject) =>{
      if(eventObject.which == 13)
        onAnswer(eventObject)
    })
    questionAnswer.focus()
  }

  var answerStarted = false;
  def onAnswer(e: JQueryEventObject): Unit =
  {
    if(questionAnswer.value() == "")
      return
    if(answerStarted)
      return
    answerStarted = true;

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

    var offset = jQuery("#afterTranslations").offset().asInstanceOf[JqOffset]

    val animateSettings = js.Dynamic.literal(
      scrollTop = offset.top
    )

    g.console.log(animateSettings);
    jQuery("html, body").animate(animateSettings, "slow");

    val gotonext = jQuery("#gotoNext");
    val href = gotonext.attr("href")
    jQuery("body").keyup((eventObject: JQueryEventObject) =>{
      if(eventObject.which == 13)
        g.window.location.href = href

    })
  }

  def getUrl: String
  def createData: js.Dynamic
}
