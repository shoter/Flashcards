package Flashcards.Jquery


import org.scalajs.jquery.JQueryStatic

import scala.scalajs.js

@js.native
trait MyQuery extends js.Object with JQueryStatic   {
  def templates(selector: String): JsTemplate = js.native;
}

package object myquery {
  val jQuery: MyQuery = js.Dynamic.global.jQuery.asInstanceOf[MyQuery]
}