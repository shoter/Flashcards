﻿using Services.Session.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISessionService
    {
        UserInfo UserInfo { get; }
        string UserID { get; set; }
        int LanguageID { get; set; }
    }
}
