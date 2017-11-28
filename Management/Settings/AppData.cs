using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Settings
{
    public static class AppData
    {
        public static string UserSettings => GetPath("settings.xml");
        public static string GetPath(string relativeAppDataPath)
        {
            return Path.Combine(Environment.GetFolderPath(
    Environment.SpecialFolder.ApplicationData), relativeAppDataPath);
        }
    }
}
