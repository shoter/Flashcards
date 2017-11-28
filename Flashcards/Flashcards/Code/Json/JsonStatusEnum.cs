using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flashcards.Code.Json
{
    public enum JsonStatusEnum
    {
        Success = 1,
        Redirect = 2,

        Error = 100,

        Warning = 200,
    }
}