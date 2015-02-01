using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace StockChartControl.Charts
{
    class LineChart : IChartDrawing
    {
        public void Draw(DrawingContext dc, List<Point> filteredPoints)
        {
            var geometry = new StreamGeometry();
            using (StreamGeometryContext context = geometry.Open())
            {
                context.BeginFigure(filteredPoints[0], false, false);
                context.PolyLineTo(filteredPoints, true, true);
            }
            geometry.Freeze();

            Pen pen = new Pen(Brushes.BlueViolet, 1);

            dc.DrawGeometry(null, pen, geometry);
        }
    }
}
