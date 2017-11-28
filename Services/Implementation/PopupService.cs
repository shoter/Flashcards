using Services.Enums;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class PopupService : IPopupService
    {
        public List<PopupMessageViewModel> Popups { get; protected set; } = new List<PopupMessageViewModel>();

        public void AddMessage(PopupMessageViewModel msg)
        {
            Popups.Add(msg);
        }

        public void AddMessage(string content, PopupMessageType msgType)
        {
            AddMessage(new PopupMessageViewModel(content, msgType));
        }

        public void AddInfo(string content)
        {
            AddMessage(content, PopupMessageType.Info);
        }

        public void AddSuccess(string content)
        {
            AddMessage(content, PopupMessageType.Success);
        }
    }
}
