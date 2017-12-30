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

class TrainingQuestion extends BaseQuestion {
  var trainingID = jQuery("#TrainingID").value()

  override def getUrl : String = Url.CreateAddress("Answer", "Training")
  override def createData: js.Dynamic =
  {
    val answer = answerInput.value()
    return js.Dynamic.literal(
      trainingID = trainingID,
      trainingCardID = flashcardID,
      answer = answer,
    );
  }
}
