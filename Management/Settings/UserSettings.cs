using Common.Xml;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Management.Settings
{
    public class UserSettings
    {
        private UserSettings() { }

        private static UserSettings current = new UserSettings();
        public static UserSettings Current => current;

        private static SerializableDictionary settings = new SerializableDictionary();

        private static void SetValue(object value, [CallerMemberName] string key = "")
        {
            if (settings.ContainsKey(key))
                settings[key] = value;
            else
                settings.Add(key, value);
        }

        private static object GetValue(object defaultValue, [CallerMemberName] string key = "")
        {
            if (settings.ContainsKey(key) == false)
                return defaultValue;

            return settings[key];
        }

        public static void SaveToXml(IXmlService xmlService, string path)
        {
            xmlService.SaveToXml(path, settings);
        }

        public static void FillEmptyDefaults()
        {
            Type type = typeof(UserSettings);
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (FieldInfo fi in fields)
            {
                var key = fi.Name;
                if (settings.ContainsKey(key) == false)
                    SetValue(fi.GetValue(null), key); //it will set default value
            }
        }

        public static void LoadFromXml(IXmlService xmlService, string path)
        {
            if(File.Exists(path))
                 settings = xmlService.LoadFromXml<SerializableDictionary>(path);
            FillEmptyDefaults();
        }

        public int WindowX
        {
            get => (int)GetValue(0);
            set => SetValue(value);
        }


        public int WindowY
        {
            get => (int)GetValue(0);
            set => SetValue(value);
        }

        public int WindowWidth
        {
            get => (int)GetValue(640);
            set => SetValue(value);
        }

        public int WindowHeight
        {
            get => (int)GetValue(480);
            set => SetValue(value);
        }

        public GridLength FirstColumnWidth
        {
            get => ((GridLengthData)GetValue(new GridLengthData(1, GridUnitType.Star))).GetGridLength();
            set => SetValue(new GridLengthData(value.Value, value.GridUnitType));
        }

        public GridLength SecondColumnWidth
        {
            get => ((GridLengthData)GetValue(new GridLengthData(1, GridUnitType.Star))).GetGridLength();
            set => SetValue(new GridLengthData(value.Value, value.GridUnitType));
        }

        public GridLength FirstRowHeight
        {
            get => ((GridLengthData)GetValue(new GridLengthData(1, GridUnitType.Star))).GetGridLength();
            set => SetValue(new GridLengthData(value.Value, value.GridUnitType));
        }


        public GridLength SecondRowHeight
        {
            get => ((GridLengthData)GetValue(new GridLengthData(1, GridUnitType.Star))).GetGridLength();
            set => SetValue(new GridLengthData(value.Value, value.GridUnitType));
        }


    }
}
