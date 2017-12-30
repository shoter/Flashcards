package Flashcards.Management
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
class Index {
  val template = jQuery.templates("#flashcardTemplate")
  val flashcardList = jQuery(".flashcardsList")
  def Run(): Unit =
  {
    jQuery("#searchForFlashcards").change((JQueryEventObject) => onSearch(JQueryEventObject))
    jQuery("#searchForFlashcards").keyup((JqueryEventObject) =>{
      if(JqueryEventObject.which == 13)
        onSearch(JqueryEventObject)
    })
  }

  def onSearch(e : JQueryEventObject) :Unit =
  {
      val value = jQuery(e.currentTarget).value()
      val url = Url.CreateAddress("Search", "Management")
      val data = js.Dynamic.literal(
        query = value
      )

    var settings = js.Dynamic.literal(
      url = url,
      data = data,
      success = onSearchSuccess _,
      `type` = "POST"
    ).asInstanceOf[JQueryAjaxSettings]

    jQuery.ajax(settings)


  }

  def onSearchSuccess(data : js.Any): Unit = {
    val results = data.asInstanceOf[JsonData].Data.asInstanceOf[js.Array[BasicSearchResult]]

    flashcardList.html("");
    for(result <- results)  {
      g.console.log(template.render(result))
      jQuery(template.render(result)).appendTo(flashcardList)
    }
  }

}
