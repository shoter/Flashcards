using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Management.Resources
{
    public static class Images
    {
        public static BitmapImage Placeholder { get; private set; }

        public static void Init()
        {
            Placeholder = new BitmapImage(new Uri(@"./Content/Images/placeholder.png", UriKind.Relative));
        }
     
    }
}
