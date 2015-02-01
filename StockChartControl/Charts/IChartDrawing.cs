using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace StockChartControl.Charts
{
    public interface IChartDrawing
    {
        void Draw(DrawingContext drawingContext, List<Point> filteredPoints);
    }
}
