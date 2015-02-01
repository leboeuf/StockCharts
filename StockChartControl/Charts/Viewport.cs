using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockChartControl.Charts
{
    /// <summary>
    /// Class that contains viewport information and handling.
    /// </summary>
    public class Viewport
    {
        /// <summary>
        /// Visible area of the chart, in viewport coordinates.
        /// </summary>
        public Rect Visible { get; set; }

        /// <summary>
        /// Screen rectangle to draw Visible to, in screen coordinates.
        /// </summary>
        public Rect Output { get; set; }
    }
}
