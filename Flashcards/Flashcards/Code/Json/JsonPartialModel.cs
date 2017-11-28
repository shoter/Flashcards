using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flashcards.Code.Json
{
    public class JsonPartialModel : JsonModelBase
    {
        public string Content { get; set; }

        public JsonPartialModel(string content)
        {
            Status = JsonStatusEnum.Success;
            Content = content;
        }

    }
}