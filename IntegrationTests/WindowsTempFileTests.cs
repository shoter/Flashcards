using Common.Temporary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [TestClass]
    public class WindowsTempFileTests
    {
        [TestMethod]
        public void ExistTest()
        {
            using (var temp = new WindowsTempFile())
                Assert.IsTrue(File.Exists(temp.Path));
        }

        [TestMethod]
        public void DeleteTest()
        {
            string path = null;
            using (var temp = new WindowsTempFile()) path = temp.Path;
            Assert.IsFalse(File.Exists(path));
        }

        [TestMethod]
        public void WriteLoadTest()
        {
            string content = "trello";
            string loadedContent;
            using (var temp = new WindowsTempFile())
            {
                temp.Save(content);
                loadedContent = temp.Load();
            }

            Assert.AreEqual(content, loadedContent);
        }

        [TestMethod]
        public void WriteLoadStreamTest()
        {
            string content = "trello";
            string loadedContent;
            using (var temp = new WindowsTempFile())
            using (var stream = generateStreamFromString(content))
            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream, Encoding.UTF8))
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                temp.Save(stream);
                temp.LoadTo(memoryStream);
                memoryStream.Flush(); //clears buffer
                memoryStream.Position = 0; //set position to 0 to read all contents
                loadedContent = streamReader.ReadToEnd();
            }
            Assert.AreEqual(content, loadedContent);
        }

        private static Stream generateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
