package Flashcards.Jquery
import Flashcards.Http
import Flashcards.Http.HttpMethod
import org.scalajs.dom
import org.scalajs.jquery.JQueryXHR

import scalajs.js;

@js.native
class AjaxOptions extends js.Object {
  val url: String = js.native
  val success: js.Function1[js.Any, Unit] = js.native
  val complete: js.Function2[JQueryXHR, String, Unit] = js.native
  val data: js.Dynamic = js.native
  val methodType : String = js.native

}

object AjaxOptions {
  def apply(httpMethod: HttpMethod, url: String, success: js.Function1[js.Any, Unit]): AjaxOptions = {
    js.Dynamic.literal(methodType = httpMethod.toString, url = url, success = success).asInstanceOf[AjaxOptions]
  }

  def apply(httpMethod: HttpMethod, url: String, data : js.Any, success: js.Function1[js.Any, Unit]) : AjaxOptions = {
    js.Dynamic.literal(methodType = httpMethod.toString, url = url, data = data, success = success).asInstanceOf[AjaxOptions]
  }
}
