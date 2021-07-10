using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Task_3.WPF_Styles
{
    public class AdjustedScrollBar: ScrollBar
    {
        public static readonly DependencyProperty ThumbColorProperty = DependencyProperty.RegisterAttached(
            "ThumbColor", typeof(Brush), typeof(AdjustedScrollViewer));

        public static void SetThumbColor(UIElement element, Brush value)
        {
            element.SetValue(ThumbColorProperty, value);
        }
        public static Brush GetThumbColor(UIElement element)
        {
            return (Brush)element.GetValue(ThumbColorProperty);
        }

    }
}