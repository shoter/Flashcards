using Common.Temporary;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Services.Implementation
{
    public class XmlService : IXmlService
    {
        public T LoadFromXml<T>(string path)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (TextReader tr = new StreamReader(path))
                return (T)xmlSerializer.Deserialize(tr);

        }

        public void SaveToXml<T>(string path, T t)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var tempFile = new WindowsTempFile())
            {
                using (TextWriter tw = new StreamWriter(tempFile.Path))
                    xmlSerializer.Serialize(tw, t);

                File.Copy(tempFile.Path, path, overwrite: true);
            }
                
        }
    }
}
