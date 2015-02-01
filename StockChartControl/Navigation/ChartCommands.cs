using System.Windows.Input;

namespace StockChartControl.Navigation
{
    public static class ChartCommands
    {
        private static RoutedUICommand CreateCommand(string name, params Key[] keys)
        {
            var gestures = new InputGestureCollection();
            foreach (var key in keys)
                gestures.Add(new KeyGesture(key));

            return new RoutedUICommand(name, name, typeof(ChartCommands), gestures);
        }

        #region Commands
        private static readonly RoutedUICommand scrollLeft = CreateCommand("ScrollLeft", Key.Right);
        public static RoutedUICommand ScrollLeft
        {
            get { return ChartCommands.scrollLeft; }
        }

        private static readonly RoutedUICommand scrollRight = CreateCommand("ScrollRight", Key.Left);
        public static RoutedUICommand ScrollRight
        {
            get { return ChartCommands.scrollRight; }
        }

        private static readonly RoutedUICommand scrollUp = CreateCommand("ScrollUp", Key.Down);
        public static RoutedUICommand ScrollUp
        {
            get { return ChartCommands.scrollUp; }
        }

        private static readonly RoutedUICommand scrollDown = CreateCommand("ScrollDown", Key.Up);
        public static RoutedUICommand ScrollDown
        {
            get { return ChartCommands.scrollDown; }
        }
        #endregion
    }
}
