using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.ViewModels;

namespace UI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MainViewModel _mainViewModel;

    public MainWindow()
    {
        InitializeComponent();

        _mainViewModel = new(Dispatcher);

        DataContext = _mainViewModel;
    }

    private void Grid_Loaded(object sender, RoutedEventArgs e)
    {
        _mainViewModel.ExecuteCommand.Execute(Array.Empty<object>());
    }
}