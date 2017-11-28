using FlashcardCommon;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Utils
{
    public class SociatisNinject
    {
        private static IKernel kernel;
        public static IKernel Current => kernel ?? (kernel = Init());



        public static IKernel Init()
        {
            var kernel = new StandardKernel();

            NinjectCommon.RegisterServices(kernel);

            return kernel;
        }
    }
}
