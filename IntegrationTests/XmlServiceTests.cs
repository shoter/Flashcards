using Common.Temporary;
using IntegrationTests.TestClasses.XmlTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [TestClass]
    public class XmlServiceTests
    {
        public XmlService xmlService = new XmlService();

        [TestMethod]
        public void TestSimpleSaveLoad()
        {
            var simple = new SimpleXmlClass()
            {
                Name = "Grzegorz",
                Number = 33
            };

            using (var temp = new WindowsTempFile())
            {
                xmlService.SaveToXml(temp.Path, simple);
                var loaded = xmlService.LoadFromXml<SimpleXmlClass>(temp.Path);

                Assert.AreEqual(simple.Name, loaded.Name);
                Assert.AreEqual(simple.Number, loaded.Number);
            }

        }

        [TestMethod]
        public void TestListSaveLoad()
        {
            var simple = new XmlClassWithList()
            {
                Name = "Grzegorz",
                Numbers = new List<int>()
                {
                    1,
                    3,
                    5,
                }
            };

            using (var temp = new WindowsTempFile())
            {
                xmlService.SaveToXml(temp.Path, simple);
                var loaded = xmlService.LoadFromXml<XmlClassWithList>(temp.Path);

                Assert.AreEqual(simple.Name, loaded.Name);
                CollectionAssert.AreEqual(simple.Numbers, loaded.Numbers);
            }
        }

        [TestMethod]
        public void TestNestedSaveLoad()
        {
            var simple = new XmlNestedClass()
            {
                Name = "Grzegorz",
                Nested = new SimpleXmlClass()
                {
                    Name = "Brzęczyszczykiewicz",
                    Number = 5
                }
            };

            using (var temp = new WindowsTempFile())
            {
                xmlService.SaveToXml(temp.Path, simple);
                var loaded = xmlService.LoadFromXml<XmlNestedClass>(temp.Path);

                Assert.AreEqual(simple.Name, loaded.Name);
                Assert.AreEqual(simple.Nested.Name, loaded.Nested.Name);
                Assert.AreEqual(simple.Nested.Number, loaded.Nested.Number);
            }
        }
    }
}
