using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using StockChartControl.Charts;
using StockChartControl.Enums;
using StockChartControl.Model;
using StockChartControl.Transforms;

namespace StockChartControl.UIElements
{
    /// <summary>
    /// Visual element that can contain chart data, technical indicators and technical analysis drawings.
    /// A chart is composed of one or more ChartPanels.
    /// </summary>
    public class ChartPanel : Canvas
    {
        private DrawingGroup GraphContents;
        private SeriesType SeriesType;
        private IndicatorType? IndicatorType;
        private IChartDrawing ChartDrawing;
        private List<BarData> ChartData;
        private List<Point> FilteredPoints;

        public ChartPanel(ChartOptions options)
        {
            this.SeriesType = options.SeriesType;
            this.IndicatorType = options.IndicatorType;
            this.ChartDrawing = ChartDrawingHelper.GetChartDrawing(SeriesType, IndicatorType);
            this.ChartData = options.ChartData;
            Update();
        }

        private void Update()
        {
            if (ChartData == null) return;

            IEnumerable<Point> points = GetPoints();

            // Transform chart data to screen points
            var transform = GetTransform();
            List<Point> transformedPoints = transform.DataToScreen(points);

            // Filter unnecessary points
            //FilteredPoints = new FilteredPointList(FilterPoints(transformedPoints), output.Left, output.Right);
            FilteredPoints = transformedPoints;
        }

        private IEnumerable<Point> GetPoints()
        {
            var res = new List<Point>();
            for (int i = 0; i < ChartData.Count; i++)
            {
                res.Add(new Point(i, ChartData[i].ClosePrice ?? 0));
            }

            return res;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (ChartData == null) return;

            if (GraphContents == null)
				GraphContents = new DrawingGroup();

            using (DrawingContext context = GraphContents.Open())
            {
                var bounds = new Rect(0, 0, 800, 600);
                var brush = new SolidColorBrush(Colors.LightGray);
                context.DrawRectangle(brush, null, bounds);

                ChartDrawing.Draw(context, FilteredPoints);
            }

            drawingContext.DrawDrawing(GraphContents);
        }

        protected CoordinateTransform GetTransform()
        {
            if (ChartData == null) return null;

            var transform = new CoordinateTransform(new Rect(0, 0, 800, 600), new Rect(0, 0, 800, 600));

            return transform;
        }

    }
}
