using System.Windows;
using StockChartControl.Transforms;

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

        public void Zoom(double factor)
        {
            Rect visible = Visible;
            Rect oldVisible = visible;
            Point center = visible.GetCenter();
            Vector halfSize = new Vector(visible.Width * factor / 2, visible.Height * factor / 2);
            Visible = new Rect(center - halfSize, center + halfSize);
        }
    }
}
