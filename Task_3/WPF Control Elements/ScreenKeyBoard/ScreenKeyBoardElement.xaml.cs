using System.Windows;
using System.Windows.Controls;

namespace Task_3.WPF_Control_Elements.ScreenKeyBoard
{
    public partial class ScreenKeyBoardElement : UserControl
    {
        public ScreenKeyBoardElement()
        {
            InitializeComponent();
        }
        
        private void _btnAlphaKeyboard_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.KeyboardState = Rife.Keyboard.KeyboardState.AlphaNumeric;
        }

        private void _btnNumericKeyboard_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.KeyboardState = Rife.Keyboard.KeyboardState.Numeric;
        }
    }
}