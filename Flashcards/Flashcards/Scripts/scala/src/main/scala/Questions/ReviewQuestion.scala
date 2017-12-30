package Flashcards.Questions

import Flashcards.Core.Url
import Flashcards.Jquery.myquery.jQuery
import Flashcards.Questions.BaseQuestion

import scala.scalajs.js

class ReviewQuestion extends BaseQuestion {
  var reviewID = jQuery("#ReviewID").value()

  override def getUrl : String = Url.CreateAddress("Answer", "Review")
  override def createData: js.Dynamic =
  {
    val answer = answerInput.value()
    return js.Dynamic.literal(
      reviewID = reviewID,
      flashcardID = flashcardID,
      answer = answer,
    );
  }
}