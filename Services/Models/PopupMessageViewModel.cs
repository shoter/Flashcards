using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class PopupMessageViewModel
    {
        public string Content { get; set; }
        public PopupMessageType MessageType { get; set; }

        public PopupMessageViewModel(string content, PopupMessageType messageType = PopupMessageType.Info)
        {
            Content = content;
            MessageType = messageType;
        }

        public PopupMessageViewModel() { }
    }
}
