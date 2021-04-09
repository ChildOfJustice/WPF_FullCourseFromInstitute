using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Task_3.WPF_Control_Elements.Spinner
{
    public partial class CircularProgressBar : UserControl
    {
        public static readonly DependencyProperty CirclesQuantityProperty =
            DependencyProperty.Register("CirclesQuantity", typeof(int), typeof(CircularProgressBar), new UIPropertyMetadata(9));
        public static readonly DependencyProperty CirclesSizeProperty =
            DependencyProperty.Register("CirclesSize", typeof(int), typeof(CircularProgressBar), new UIPropertyMetadata(20));

        private ObservableCollection<Ellipse> _ellipses;
        private int _transformAngle;
        
        private readonly DispatcherTimer _animationTimer;
        
        
        public CircularProgressBar()
        {
            InitializeComponent();
            TransformAngle = 0;

            IsVisibleChanged += OnVisibleChanged;

            _animationTimer = new DispatcherTimer(DispatcherPriority.ContextIdle, Dispatcher)
            {
                Interval = new TimeSpan(0, 0, 0, 0, 175)
            };
            
            InitEllipses();
            
        }
        
        public int CirclesQuantity
        {
            get { return (int)GetValue(CirclesQuantityProperty); }
            set { SetValue(CirclesQuantityProperty, value); }
        }
        public int CirclesSize
        {
            get { return (int)GetValue(CirclesSizeProperty); }
            set { SetValue(CirclesSizeProperty, value); }
        }

        public int TransformAngle
        {
            get =>
                _transformAngle;

            private set
            {
                _transformAngle = value;
                //OnPropertyChanged(nameof(Ellipses));
            }
        }
        public ObservableCollection<Ellipse> Ellipses
        {
            get =>
                _ellipses;

            private set
            {
                _ellipses = value;
                //OnPropertyChanged(nameof(Ellipses));
            }
        }
        private static void SetPosition(DependencyObject ellipse, double offset, double posOffSet, double step)
        {
            //50 + 
            ellipse.SetValue(Canvas.LeftProperty, (Math.Sin(offset + (posOffSet * step)) * 50));
            ellipse.SetValue(Canvas.TopProperty, (Math.Cos(offset + (posOffSet * step)) * 50));
           
        }
        private void Start()
        {
            _animationTimer.Tick += OnAnimationTick;
            _animationTimer.Start();
        }
        private void Stop()
        {
            _animationTimer.Stop();
            _animationTimer.Tick -= OnAnimationTick;
        }
        private void OnAnimationTick(object sender, EventArgs e)
        {
            _transformAngle = (_transformAngle + 36) % 360;
            // _ellipses.Add(new Ellipse());
            _ellipses = null;
            //System.Windows.MessageBox.Show(_transformAngle.ToString());
            //_spinnerRotate.Angle = (_spinnerRotate.Angle + 36) % 360;
            //_spinnerRotate2.Angle = (_spinnerRotate2.Angle + 36) % 360;
        }

        

        private void InitEllipses()
        {
            Ellipses = new ObservableCollection<Ellipse>();
            
            const double offset = Math.PI;
            double step = Math.PI * 2 / (double)(CirclesQuantity + 1);
            double opacityStep = 1.0 / CirclesQuantity;
            // var ellipse = new Ellipse();
            // ellipse.Width = 20;
            // ellipse.Height = 20;
            // ellipse.Stretch = Stretch.Fill;
            // ellipse.Fill = new SolidColorBrush(Color.FromRgb(100,20,250));
            // Ellipses.Add(ellipse);
            //
            
            for (int i = 0; i < CirclesQuantity; i++)
            {
                var ellipse = new Ellipse();
                Canvas.SetLeft(ellipse, 0);
                ellipse.Width = CirclesSize;
                ellipse.Height = CirclesSize;
                ellipse.Stretch = Stretch.Fill;
                ellipse.Fill = new SolidColorBrush(Color.FromRgb(100,20,250));
                Ellipses.Add(ellipse);
                
                
                // var newCircle = new Ellipse();
                // newCircle.Width = CirclesSize;
                // newCircle.Height = CirclesSize;
                // newCircle.SetValue(Canvas.LeftProperty, 0);
                // newCircle.SetValue(Canvas.TopProperty, 0);
                // newCircle.Stretch = Stretch.Fill;
                // newCircle.Opacity = 1.0 - i * opacityStep;
                // SetPosition(newCircle, offset, i, step);
                // Ellipses.Add(newCircle);
            }
        }
        
        private void OnCanvasLoaded(object sender, RoutedEventArgs e)
        {
            const double offset = Math.PI;
            double step = Math.PI * 2 / (double)(CirclesQuantity + 1);
            double opacityStep = 1.0 / CirclesQuantity;


            //MessageBox.Show("Hello, world!", "My App");
            int i = 0;
            foreach (var ellipse in Ellipses)
            {
                // ellipse.SetValue(Canvas.LeftProperty, 0);
                // ellipse.SetValue(Canvas.TopProperty, 0);
                SetPosition(ellipse, offset, i, step);
                i++;
            }
            
            
            // SetPosition(_circle0, offset, 0.0, step);
            // SetPosition(_circle1, offset, 1.0, step);
            // SetPosition(_circle2, offset, 2.0, step);
            // SetPosition(_circle3, offset, 3.0, step);
            // SetPosition(_circle4, offset, 4.0, step);
            // SetPosition(_circle5, offset, 5.0, step);
            // SetPosition(_circle6, offset, 6.0, step);
            // SetPosition(_circle7, offset, 7.0, step);
            // SetPosition(_circle8, offset, 8.0, step);
        }
        
        private void OnCanvasUnloaded(object sender, RoutedEventArgs e)
        {
            Stop();
        }
        
        private void OnVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var isVisible = (bool)e.NewValue;

            if (isVisible)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }
        
        
    }
}