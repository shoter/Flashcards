package Flashcards.Core

import scala.scalajs.js.annotation.JSExportTopLevel
import org.scalajs.dom
import dom.document
import org.scalajs.jquery.jQuery

object Main {
def main(args: Array[String]): Unit = {
    jQuery("#click-me-button").click(() => addClickedMessage())
  }

  @JSExportTopLevel("InitFlashcard")
  def Init(moduleName: String): Unit =
  {
    moduleName match
      {
      case "Management.Index" => {
        new  Flashcards.Management.Index().Run()
      }
    }

  }


    @JSExportTopLevel("addClickedMessage")
    def addClickedMessage(): Unit = {
      jQuery("body").append("<p>Hello World</p>")
    }
}
