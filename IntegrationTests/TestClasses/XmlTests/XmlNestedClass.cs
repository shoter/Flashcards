using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.TestClasses.XmlTests
{
    public class XmlNestedClass
    {
        public string Name { get; set; }
        public SimpleXmlClass Nested { get; set; }
    }
}
