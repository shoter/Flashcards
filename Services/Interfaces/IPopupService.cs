using Services.Enums;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPopupService
    {
        List<PopupMessageViewModel> Popups { get; }

        void AddMessage(PopupMessageViewModel msg);
        void AddMessage(string content, PopupMessageType msgType);
        void AddInfo(string content);
        void AddSuccess(string content);
    }
}
