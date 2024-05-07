using System.Windows;
using System.Windows.Controls;
using UI.ViewModels;

namespace UI.Controls;
/// <summary>
/// Interaction logic for ConsoleControl.xaml
/// </summary>
public partial class ConsoleControl : UserControl
{
    public ConsoleControl()
    {
        InitializeComponent();
    }

    private async void ViewModel_ItemAdded(object newItem)
    {
        await Task.Delay(1);
        listView.ScrollIntoView(newItem);
    }

    private void listView_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is ConsoleViewModel)
            ((ConsoleViewModel)DataContext).ItemAdded += ViewModel_ItemAdded;
    }

    private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        listView.SelectedItem = null;
    }
}
