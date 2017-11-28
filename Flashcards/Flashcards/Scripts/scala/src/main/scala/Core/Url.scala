package Flashcards.Core
import scalajs.js
import js.Dynamic.{global => g}
import scala.util.matching.Regex

object Url {


  def CreateAddress(actionName : String, controllerName : String): String =
  {
    var url = g.window.location.href.asInstanceOf[String];
    val pattern = new Regex("\\/([\\w?=\\-?]+\\/?)+$")

    var urlBuilder = new StringBuilder;

    urlBuilder.append(pattern replaceFirstIn(url, ""));
    urlBuilder.append("/");
    urlBuilder.append(controllerName);
    urlBuilder.append("/");
    urlBuilder.append(actionName);

    return urlBuilder.toString
  }

}
