using System.Windows.Controls;

namespace UI.Controls
{
    /// <summary>
    /// Interaction logic for InputControl.xaml
    /// </summary>
    public partial class InputControl : UserControl
    {
        public InputControl()
        {
            InitializeComponent();
        }

        private void input_Loaded(object sender, System.Windows.RoutedEventArgs e) => input.Focus();
    }
}
