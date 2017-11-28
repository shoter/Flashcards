using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flashcards.Code.Json
{
    public class JsonDebugErrorModel : JsonErrorModel
    {
        public JsonDebugErrorModel(string message) : base()
        {
#if DEBUG
            ErrorMessage = message;
#else
            ErrorMessage = "Error";
#endif
        }
    }
}