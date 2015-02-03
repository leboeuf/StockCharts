using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using StockChartControl.Enums;
using StockChartControl.Model;
using StockChartControl.Themes.ChartStyles;
using StockChartControl.Transforms;

namespace StockChartControl.Charts
{
    /// <summary>
    /// Visual element that can contain chart data, technical indicators and technical analysis drawings.
    /// A chart is composed of one or more ChartPanels.
    /// </summary>
    public class ChartPanel : Canvas
    {
        private DrawingGroup GraphContents;
        private Viewport Viewport = new Viewport();
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
            Viewport.OnViewportResized(ActualWidth, ActualHeight);
            Update();
        }

        private void Update()
        {
            if (ChartData == null) return;

            IEnumerable<Point> points = GetPoints();

            // Transform chart data to screen points
            Transform = new CoordinateTransform(Viewport.Visible, Viewport.Output);
            List<Point> transformedPoints = Transform.DataToScreen(points);

            // Filter unnecessary points
            //FilteredPoints = new FilteredPointList(FilterPoints(transformedPoints), Viewport.Output.Left, Viewport.Output.Right);
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
            {
                // ActualWidth and ActualHeight are NaN at initialization, need to do this here
                Viewport.Visible = new Rect(new Size(ActualWidth, ActualHeight));
                Viewport.OnViewportResized(ActualWidth, ActualHeight);
                GraphContents = new DrawingGroup();
            }

            if (FilteredPoints == null)
                Update();

            using (DrawingContext context = GraphContents.Open())
            {
                // Draw Background
                context.DrawRectangle(ChartStyle.BackgroundColor, null, Viewport.Output);

                // Draw Chart
                ChartDrawing.Draw(context, FilteredPoints, ChartStyle);

                //Draw Debug text
                context.DrawText(new FormattedText("Viewport.Visible=" + Viewport.Visible + "\n" + "Viewport.Output=" + Viewport.Output,
                    CultureInfo.InvariantCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Arial"), 12, Brushes.Red),
                    new Point(10, 10));
            }

            drawingContext.DrawDrawing(GraphContents);
        }

        #region Commands implementation

        /// <summary>
        /// Must run after a command is executed to refresh the chart.
        /// </summary>
        private void CommandExecuted()
        {
            Update();
            InvalidateVisual();
        }

        public void ScrollVisible(double xShiftCoeff, double yShiftCoeff)
        {
            Rect visible = Viewport.Visible;
            Rect oldVisible = visible;
            double width = visible.Width;
            double height = visible.Height;

            visible.Offset(xShiftCoeff * width, yShiftCoeff * height);

            Viewport.Visible = visible;
            CommandExecuted();
        }

        public void Zoom(double zoomeCoeff)
        {
            Viewport.Zoom(zoomeCoeff);
            CommandExecuted();
        }

        public void ZoomToPoint(double coeff)
        {
            Point pt = Mouse.GetPosition(this);
            Point dataPoint = Transform.ScreenToData(pt);
            Rect visible = Viewport.Visible;

            Viewport.Visible = visible.Zoom(dataPoint, coeff);
            CommandExecuted();
        }

        public void ZoomWithParamExecute(double zoomParam)
        {
            Viewport.Zoom(zoomParam);
            CommandExecuted();
        }

        public void FitToView()
        {
            //Viewport.FitToView();// todo
            CommandExecuted();
        }

        #endregion
    }
}
