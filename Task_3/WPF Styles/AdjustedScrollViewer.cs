using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Task_3.WPF_Styles
{
    public class AdjustedScrollViewer// : ScrollViewer
    {
        public static readonly DependencyProperty ThumbColorProperty = DependencyProperty.RegisterAttached(
            "ThumbColor", typeof(Brush), typeof(AdjustedScrollViewer));
        // public static readonly DependencyProperty ThumbColorProperty = DependencyProperty.RegisterAttached(
        //     "ThumbColor",
        //     typeof(Color),
        //     typeof(AdjustedScrollViewer),
        //     new PropertyMetadata(Color.FromRgb(220, 55, 100))
        // );
        
        public static void SetThumbColor(UIElement element, Brush value)
        {
            element.SetValue(ThumbColorProperty, value);
            MessageBox.Show("!!!");
        }
        public static Brush GetThumbColor(UIElement element)
        {
            MessageBox.Show("GET");
            return (Brush)element.GetValue(ThumbColorProperty);
            
        }

        // public AdjustedScrollViewer()
        // {
        //     Loaded += OnLoad;
        // }
        
        // private void OnLoad(object sender, RoutedEventArgs e)
        // {
        //     MessageBox.Show("!!!!");
        //     
        // }
    }
}