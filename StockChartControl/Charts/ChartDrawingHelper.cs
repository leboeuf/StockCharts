using System;
using StockChartControl.Enums;

namespace StockChartControl.Charts
{
    public static class ChartDrawingHelper
    {
        /// <summary>
        /// Gets the drawing class for this series or indicator.
        /// </summary>
        /// <param name="seriesType"></param>
        /// <param name="indicatorType"></param>
        /// <returns></returns>
        public static IChartDrawing GetChartDrawing(SeriesType seriesType, IndicatorType? indicatorType)
        {
            switch (seriesType)
            {
                case SeriesType.LineChart: return new LineChart();
            }

            throw new ArgumentException("No IChartDrawing found for this SeriesType and IndicatorType combination.");
        }
    }
}
