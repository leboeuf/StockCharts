using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using StockChartControl.Model;

namespace StockChartControl.Charts
{
    public interface IChartDrawing
    {
        void Draw(DrawingContext drawingContext, List<Point> filteredPoints, ChartStyle chartStyle);
    }
}
