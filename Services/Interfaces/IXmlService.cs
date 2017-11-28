using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IXmlService
    {
        void SaveToXml<T>(string path, T t);
        T LoadFromXml<T>(string path);
    }
}
