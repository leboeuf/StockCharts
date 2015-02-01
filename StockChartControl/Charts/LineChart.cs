using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using StockChartControl.Model;

namespace StockChartControl.Charts
{
    class LineChart : IChartDrawing
    {
        public void Draw(DrawingContext dc, List<Point> filteredPoints, ChartStyle chartStyle)
        {
            var geometry = new StreamGeometry();
            using (StreamGeometryContext context = geometry.Open())
            {
                context.BeginFigure(filteredPoints[0], false, false);
                context.PolyLineTo(filteredPoints, true, true);
            }
            geometry.Freeze();

            Pen pen = new Pen(chartStyle.LineColor, 1);

            dc.DrawGeometry(null, pen, geometry);
        }
    }
}
