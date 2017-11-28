package Flashcards.Http

sealed case class HttpMethod(method: String)
{
  override def toString = method
}

object HttpMethod{
  object Get extends HttpMethod("GET")
  object Post extends HttpMethod("POST")
}
