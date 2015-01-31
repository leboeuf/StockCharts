using System.ComponentModel;
using System.Windows.Controls;

namespace StockChartControl.UIElements
{
    /// <summary>
    /// Visual element that can contain chart data, technical indicators and technical analysis drawings.
    /// A chart is composed of one or more ChartPanels.
    /// </summary>
    class ChartPanel : Control, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { }
            remove { }
        }
    }
}
