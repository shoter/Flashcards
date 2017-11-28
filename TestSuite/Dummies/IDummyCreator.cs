using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite.Dummies
{
    public interface IDummyCreator<T>
    {
        /// <summary>
        /// Should create object. Do not need any other method to run. It should run as by by default
        /// </summary>
        /// <returns></returns>
        T Create();
    }
}
