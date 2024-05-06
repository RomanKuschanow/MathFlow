using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SPL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace UI.ViewModels;

partial class ExecutableInstanceViewModel : ObservableObject
{
    private CancellationTokenSource? ctSource;
    private Action<ExecutableInstanceViewModel> _close;

    [ObservableProperty]
    private ConsoleViewModel consoleViewModel;

    [ObservableProperty]
    private string filePath = "";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ClearOutputCommand))]
    private bool isRunning;

    public ExecutableInstanceViewModel(Dispatcher dispatcher, string filePath, Action<ExecutableInstanceViewModel> close)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
        }

        consoleViewModel = new ConsoleViewModel(dispatcher);
        FilePath = filePath;
        _close = close;
    }

    [RelayCommand]
    private async Task Execute()
    {
        ctSource = new CancellationTokenSource();
        CancellationToken ct = ctSource.Token;

        IsRunning = true;

        string code = File.ReadAllText(FilePath);

        SPLProgram program = new(code, new() { ConsoleViewModel.Output }, ConsoleViewModel.Input);

        SPLProgram.UpdateParser();

        await program.Build(ct);
        await program.Execute(ct);

        IsRunning = false;
    }

    [RelayCommand]
    private void Cancel()
    {
        ctSource?.Cancel();
    }    
    
    [RelayCommand(CanExecute = nameof(ClearOutputCanExecute))]
    private void ClearOutput()
    {
        ConsoleViewModel.ClearOutput();
    }

    private bool ClearOutputCanExecute()
    {
        return !IsRunning;
    }

    [RelayCommand]
    private void Close()
    {
        _close(this);
    }
}
