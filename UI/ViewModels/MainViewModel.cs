using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SPL;
using System.IO;
using System.Windows.Threading;

namespace UI.ViewModels;

partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ConsoleViewModel consoleViewModel;

    public MainViewModel(Dispatcher dispatcher)
    {
        consoleViewModel = new ConsoleViewModel(dispatcher);
    }

    [RelayCommand]
    private async Task Execute()
    {
        string code = File.ReadAllText("test_code.txt");

        SPLProgram program = new(code, new() { ConsoleViewModel.Output }, ConsoleViewModel.Input);

        SPLProgram.UpdateParser();

        await program.Build();
        await program.Execute();
    }
}
