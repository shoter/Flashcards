enablePlugins(ScalaJSPlugin)
libraryDependencies += "org.scala-js" %%% "scalajs-dom" % "0.9.1"
libraryDependencies += "be.doeraene" %%% "scalajs-jquery" % "0.9.1"

name := "Flashcards"
scalaVersion := "2.12.2" // or any other Scala version >= 2.10.2

// This is an application with a main method
scalaJSUseMainModuleInitializer := true

skip in packageJSDependencies := false
jsDependencies += "org.webjars" % "jquery" % "2.1.4" / "2.1.4/jquery.js" 
jsDependencies += "org.webjars.bower" % "jsrender" % "1.0.0-rc.70" / "1.0.0-rc.70/jsrender.js"