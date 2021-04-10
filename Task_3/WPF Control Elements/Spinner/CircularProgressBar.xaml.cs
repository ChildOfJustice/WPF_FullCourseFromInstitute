using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            DependencyProperty.Register("CirclesQuantity", typeof(int), typeof(CircularProgressBar), new UIPropertyMetadata(5));
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("CirclesSize", typeof(int), typeof(CircularProgressBar), new UIPropertyMetadata(20));
        
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(int), typeof(CircularProgressBar), new UIPropertyMetadata(30));
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Brush), typeof(CircularProgressBar), new UIPropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty ClockDirectionProperty =
            DependencyProperty.Register("ClockDirection", typeof(bool), typeof(CircularProgressBar), new UIPropertyMetadata(true));
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(int), typeof(CircularProgressBar), new UIPropertyMetadata(50));

        public int CirclesQuantity
        {
            get { return (int) GetValue(CirclesQuantityProperty); }
            set { SetValue(CirclesQuantityProperty, value); }
        }
        public int CirclesSize
        {
            get { return (int)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }
        public int Speed
        {
            get { return (int) GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        public int Size
        {
            get { return (int) GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public Brush Color
        {
            get { return (Brush) GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public bool ClockDirection
        {
            get { return (bool) GetValue(ClockDirectionProperty); }
            set { SetValue(ClockDirectionProperty, value); }
        }

        public int Radius
        {
            get { return (int) GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        private ObservableCollection<Ellipse> _ellipses;
        
        // private readonly DispatcherTimer _animationTimer;
        
        
        public CircularProgressBar()
        {
            InitializeComponent();
            
            Loaded += OnCanvasLoaded;
            
            IsVisibleChanged += OnVisibleChanged;

            // _animationTimer = new DispatcherTimer(DispatcherPriority.ContextIdle, Dispatcher)
            // {
            //     Interval = new TimeSpan(0, 0, 0, 0, 175)
            // };
            
            InitEllipses();
            
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


        private static void SetPosition(DependencyObject ellipse, double x, double y)
        {
            //50 + 
            ellipse.SetValue(Canvas.LeftProperty, x);
            ellipse.SetValue(Canvas.TopProperty, y);
           
        }
        private void Start()
        {
            // _animationTimer.Tick += OnAnimationTick;
            // _animationTimer.Start();
        }
        private void Stop()
        {
            // _animationTimer.Stop();
            // _animationTimer.Tick -= OnAnimationTick;
        }
        private void OnAnimationTick(object sender, EventArgs e)
        {
            // _transformAngle = (_transformAngle + 36) % 360;
            // _ellipses.Add(new Ellipse());
            // _ellipses = null;
            //System.Windows.MessageBox.Show(_transformAngle.ToString());
            //_spinnerRotate.Angle = (_spinnerRotate.Angle + 36) % 360;
            //_spinnerRotate2.Angle = (_spinnerRotate2.Angle + 36) % 360;
        }

        

        private void InitEllipses()
        {
            _ellipses = new ObservableCollection<Ellipse>();
           
            // const double offset = Math.PI;
            // double step = Math.PI * 2 / (double)(CirclesQuantity + 1);
            // double opacityStep = 1.0 / CirclesQuantity;
            // // var ellipse = new Ellipse();
            // // ellipse.Width = 20;
            // // ellipse.Height = 20;
            // // ellipse.Stretch = Stretch.Fill;
            // // ellipse.Fill = new SolidColorBrush(Color.FromRgb(100,20,250));
            // // Ellipses.Add(ellipse);
            // //
            //
            // for (int i = 0; i < CirclesQuantity; i++)
            // {
            //     var ellipse = new Ellipse();
            //     Canvas.SetLeft(ellipse, 0);
            //     ellipse.Width = CirclesSize;
            //     ellipse.Height = CirclesSize;
            //     ellipse.Stretch = Stretch.Fill;
            //     ellipse.Fill = new SolidColorBrush(Color.FromRgb(100,20,250));
            //     Ellipses.Add(ellipse);
            //     
            //     
            //     // var newCircle = new Ellipse();
            //     // newCircle.Width = CirclesSize;
            //     // newCircle.Height = CirclesSize;
            //     // newCircle.SetValue(Canvas.LeftProperty, 0);
            //     // newCircle.SetValue(Canvas.TopProperty, 0);
            //     // newCircle.Stretch = Stretch.Fill;
            //     // newCircle.Opacity = 1.0 - i * opacityStep;
            //     // SetPosition(newCircle, offset, i, step);
            //     // Ellipses.Add(newCircle);
            // }







        }

        // private void OnMainCanvasLoaded(object sender, RoutedEventArgs e)
        // {
        //     ItemsControl.SetValue(Canvas.TopProperty, -60);
        //     ItemsControl.SetValue(Canvas.LeftProperty, -60);
        // }
        private void OnCanvasLoaded(object sender, RoutedEventArgs e)
        {

            
            double ActualSize = Size;
            for (int i = 0; i < CirclesQuantity; i++)
            {
                _ellipses.Add(new Ellipse {Height = ActualSize, Width = ActualSize, Fill = Color});
                //ActualSize -= Size / CirclesQuantity;
            }

            double CircleShift = (360) / (CirclesQuantity);

            // Items.ItemsSource = elipses;
            int xCenter = Radius;
            int yCenter = Radius;
            // int xCenter = 20;
            // int yCenter = 20;
            
            
            int dt = 30 / Speed;
            double dphi = 1;
            if (!ClockDirection) dphi *= -1;

            foreach (int i in Enumerable.Range(0, _ellipses.Count))
            {
                double phi = 0;
                _ellipses[i].Margin = GetThicknessForCirclePath(xCenter, yCenter, Radius, phi + i * CircleShift);
                
                phi %= 360;
                // DispatcherTimer timer = new DispatcherTimer();
                // timer.Tick += new EventHandler((sender2, e2) =>
                // {
                //     elipses[i].Margin = GetThicknessForCirclePath(xCenter, yCenter, Radius, phi + i * CircleShift);
                //     phi += dphi;
                //     phi %= 360;
                // });
                // timer.Interval = new TimeSpan(0, 0, 0, 0, dt);
                // timer.Start();
            }
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            //
            // double ActualSize = Size;
            // for (int i = 0; i < CirclesQuantity; i++)
            // {
            //     _ellipses.Add(new Ellipse {Height = ActualSize, Width = ActualSize, Fill = Color});
            //     //ActualSize -= Size / CirclesQuantity;
            // }
            //
            // double CircleShift = (360) / (CirclesQuantity);
            //
            //
            //
            //
            //
            //
            //
            //
            // int xCenter = -CirclesSize;
            // int yCenter = -CirclesSize;
            //
            //
            // int dt = 30 / Speed;
            // double dphi = 1;
            // if (!ClockDirection) dphi *= -1;
            // double phi = 0;
            // foreach (int i in Enumerable.Range(0, _ellipses.Count))
            // {
            //     
            //     _ellipses[i].Margin = GetThicknessForCirclePath(xCenter, yCenter, Radius, phi + i * CircleShift);
            //     phi += dphi;
            //     phi %= 360;
            //     // timer.Interval = new TimeSpan(0, 0, 0, 0, dt);
            //     // timer.Start();
            // }
            //const double offset = Math.PI;
            //double step = Math.PI * 2 / (double)(CirclesQuantity + 1);
            // double step = 360 / (double)(CirclesQuantity );
            // double opacityStep = 1.0 / CirclesQuantity;
            //
            //
            // //MessageBox.Show("Hello, world!", "My App");
            // double i = 0;
            // double x = 100;
            // double y = 0;
            // foreach (var ellipse in Ellipses)
            // {
            //     // ellipse.SetValue(Canvas.LeftProperty, 0);
            //     // ellipse.SetValue(Canvas.TopProperty, 0);
            //     //SetPosition(ellipse, x, y, i, step);
            //     i++;
            // }
            
            
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

        private void OnEllipseCanvasLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _ellipses.Count; i++)
            {
                SetPosition(_ellipses[i], -(Radius + Size/2), -(Radius + Size/2));
            }
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
        
        private Thickness GetThicknessForCirclePath(int xCenter, int yCenter, int radius, double phi)
        {
            double x = radius * Math.Cos(phi / 180.0 * Math.PI) + xCenter;
            double y = radius * Math.Sin(phi / 180.0 * Math.PI) + yCenter;
            
            return new Thickness(x, y, 0, 0);
        }
    }
}