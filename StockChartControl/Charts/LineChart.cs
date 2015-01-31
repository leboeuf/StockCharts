using System.Windows;
using System.Windows.Media;

namespace StockChartControl.Charts
{
    class LineChart : IChartDrawing
    {
        public void Draw(DrawingContext context)
        {
            var bounds = new Rect(0, 0, 40, 40);
            var brush = new SolidColorBrush(Colors.Yellow);
            context.DrawRectangle(brush, null, bounds);
        }
    }
}
