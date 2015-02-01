using System.Windows;
using System.Windows.Input;
using StockChartControl.UIElements;

namespace StockChartControl.Navigation
{
    /// <summary>
    /// Provides keyboard navigation around chart.
    /// </summary>
    public class KeyboardNavigation
    {
        private StockChartControl StockChartControl;

        public KeyboardNavigation(StockChartControl stockChartControl)
        {
            this.StockChartControl = stockChartControl;
            InitCommands();
        }

        private void InitCommands()
        {
            var ScrollLeftCommandBinding = new CommandBinding(
                ChartCommands.ScrollLeft,
                ScrollLeftExecute,
                ScrollLeftCanExecute);
            StockChartControl.CommandBindings.Add(ScrollLeftCommandBinding);

            var ScrollRightCommandBinding = new CommandBinding(
                ChartCommands.ScrollRight,
                ScrollRightExecute,
                ScrollRightCanExecute);
            StockChartControl.CommandBindings.Add(ScrollRightCommandBinding);

            var ScrollUpCommandBinding = new CommandBinding(
                ChartCommands.ScrollUp,
                ScrollUpExecute,
                ScrollUpCanExecute);
            StockChartControl.CommandBindings.Add(ScrollUpCommandBinding);

            var ScrollDownCommandBinding = new CommandBinding(
                ChartCommands.ScrollDown,
                ScrollDownExecute,
                ScrollDownCanExecute);
            StockChartControl.CommandBindings.Add(ScrollDownCommandBinding);
        }

        #region Scroll

        private double scrollCoeff = 0.05;
        private void ScrollVisibleProportionally(double xShiftCoeff, double yShiftCoeff)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(StockChartControl))
            {
                if (child is ChartPanel)
                    ((ChartPanel)child).ScrollVisible(xShiftCoeff, yShiftCoeff);
            }
        }

        #region ScrollLeft

        private void ScrollLeftExecute(object target, ExecutedRoutedEventArgs e)
        {
            ScrollVisibleProportionally(scrollCoeff, 0);
            e.Handled = true;
        }

        private void ScrollLeftCanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region ScrollRight

        private void ScrollRightExecute(object target, ExecutedRoutedEventArgs e)
        {
            ScrollVisibleProportionally(-scrollCoeff, 0);
            e.Handled = true;
        }

        private void ScrollRightCanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region ScrollUp

        private void ScrollUpExecute(object target, ExecutedRoutedEventArgs e)
        {
            ScrollVisibleProportionally(0, -scrollCoeff);
            e.Handled = true;
        }

        private void ScrollUpCanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region ScrollDown

        private void ScrollDownExecute(object target, ExecutedRoutedEventArgs e)
        {
            ScrollVisibleProportionally(0, scrollCoeff);
            e.Handled = true;
        }

        private void ScrollDownCanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #endregion
    }
}
