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

        /// <summary>
        /// Call this when the canvas size changed.
        /// </summary>
        /// <param name="width">The new canvas width.</param>
        /// <param name="height">The new canvas height.</param>
        public void OnViewportResized(double width, double height)
        {
            Output = new Rect(new Size(width, height));
        }
    }
}
