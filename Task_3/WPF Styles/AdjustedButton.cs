using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Task_3.WPF_Styles
{
    public class AdjustedButton: Button
    {
        public static readonly DependencyProperty ImageScaleXProperty = DependencyProperty.RegisterAttached(
            "ImageScaleX", typeof(int), typeof(AdjustedButton), new FrameworkPropertyMetadata(100));

        public static int GetImageScaleX(DependencyObject obj)
        {
            return (int)obj.GetValue(ImageScaleXProperty);
        }

        public static void SetImageScaleX(DependencyObject obj, int value)
        {
            obj.SetValue(ImageScaleXProperty, value);
        }
        
        public static readonly DependencyProperty ImageScaleYProperty = DependencyProperty.RegisterAttached(
            "ImageScaleY", typeof(int), typeof(AdjustedButton), new FrameworkPropertyMetadata(100));

        public static int GetImageScaleY(DependencyObject obj)
        {
            return (int)obj.GetValue(ImageScaleYProperty);
        }

        public static void SetImageScaleY(DependencyObject obj, int value)
        {
            obj.SetValue(ImageScaleYProperty, value);
        }
    }
}