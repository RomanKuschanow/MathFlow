using CommunityToolkit.Mvvm.ComponentModel;
using System.Security.RightsManagement;
using UI.Models;

namespace UI.ViewModels;

partial class ConsoleOutputViewModel : ObservableObject
{
    private ConsoleOutput _output;

    public string Text
    {
        get => _output.Text;
    }

    public ConsoleOutputViewModel(ConsoleOutput output)
    {
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }
}
