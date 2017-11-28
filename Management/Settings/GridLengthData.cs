using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Management.Settings
{
    public class GridLengthData
    {
        public double Width { get; set; }
        public GridUnitType GridUnitType { get; set; }

        public GridLengthData() { }

        public GridLengthData(double width, GridUnitType gridUnitType)
        {
            this.Width = width;
            this.GridUnitType = gridUnitType;
        }

        public GridLength GetGridLength()
        {
            return new GridLength(Width, GridUnitType);
        }
    }
}
