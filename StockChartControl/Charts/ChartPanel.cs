using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using StockChartControl.Charts;
using StockChartControl.Enums;
using StockChartControl.Model;
using StockChartControl.Themes.ChartStyles;
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
        private Rect CanvasBounds;
        private ChartStyle ChartStyle;
        private SeriesType SeriesType;
        private IndicatorType? IndicatorType;
        private IChartDrawing ChartDrawing;
        private List<BarData> ChartData;
        private List<Point> FilteredPoints;

        private CoordinateTransform Transform;

        public ChartPanel(ChartOptions options)
        {
            this.SeriesType = options.SeriesType;
            this.IndicatorType = options.IndicatorType;
            this.ChartDrawing = ChartDrawingHelper.GetChartDrawing(SeriesType, IndicatorType);
            this.ChartData = options.ChartData;
            this.ChartStyle = options.ChartStyle;

            if (this.ChartStyle == null)
                this.ChartStyle = new DefaultChartStyle();

            SizeChanged += ChartPanel_SizeChanged;

        }

        private void ChartPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            if (ChartData == null) return;

            IEnumerable<Point> points = GetPoints();

            // Transform chart data to screen points
            CanvasBounds = new Rect(new Size(ActualWidth, ActualHeight));
            Transform = new CoordinateTransform(CanvasBounds, CanvasBounds);
            List<Point> transformedPoints = Transform.DataToScreen(points);

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

            if (FilteredPoints == null) Update(); // ActualWidth and ActualHeight are NaN at initialization, need to do this here

            if (GraphContents == null)
                GraphContents = new DrawingGroup();

            using (DrawingContext context = GraphContents.Open())
            {
                // Draw Background
                context.DrawRectangle(ChartStyle.BackgroundColor, null, CanvasBounds);

                // Draw chart
                ChartDrawing.Draw(context, FilteredPoints, ChartStyle);

                #region Debug text
                //context.DrawText(new FormattedText("Debugtext", 
                //    CultureInfo.InvariantCulture, 
                //    FlowDirection.LeftToRight, 
                //    new Typeface("Arial"), 12, Brushes.Black), 
                //    new Point(10, 10));
                #endregion
            }

            drawingContext.DrawDrawing(GraphContents);
        }
    }
}
