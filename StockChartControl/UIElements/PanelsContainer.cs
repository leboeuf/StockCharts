using System.Collections.Generic;
using System.Windows.Controls;

namespace StockChartControl.UIElements
{
    class PanelsContainer : Canvas
    {
        private IEnumerable<ChartPanel> ChartPanels;

        /// <summary>
        /// Gets the collection of panels in the container.
        /// </summary>
        /// <returns>The collection of panels in the container.</returns>
        internal IEnumerable<ChartPanel> GetPanels()
        {
            return this.ChartPanels;
        }
    }
}
