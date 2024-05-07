using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ICSharpCode.AvalonEdit.Document;
using Microsoft.Win32;
using SPL;
using System.IO;
using System.Windows;
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
    private TextDocument document = new();

    public string Code => Document.Text;

    public string FileName => Document.FileName;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ClearOutputCommand))]
    private bool isRunning;

    [ObservableProperty]
    private bool isSaved;

    public ExecutableInstanceViewModel(Dispatcher dispatcher, string filePath, Action<ExecutableInstanceViewModel> close, bool isSaved = true)
    {
        if (!string.IsNullOrWhiteSpace(filePath))
        {
            Document = new(File.ReadAllText(filePath))
            {
                FileName = filePath[(filePath.LastIndexOf('\\') + 1)..]
            };
        }
        else
        {
            Document.FileName = "Unsaved *";
        }

        consoleViewModel = new ConsoleViewModel(dispatcher);
        FilePath = filePath;
        _close = close;
        this.isSaved = isSaved;

        Document.TextChanged += Document_TextChanged;
    }

    private void Document_TextChanged(object? sender, EventArgs e)
    {
        IsSaved = false;
    }

    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(FilePath))
        {
            SaveFileDialog saveFileDialog = new()
            {
                Title = "Select File",
                Filter = "All files (*.*)|*.*"
            };

            var result = saveFileDialog.ShowDialog();

            if (result.HasValue && result.Value)
                FilePath = saveFileDialog.FileName;
            else
                return;
        }
        await File.WriteAllTextAsync(FilePath, Code);

        IsSaved = true;
    }

    [RelayCommand]
    private async Task Execute()
    {
        ctSource = new CancellationTokenSource();
        CancellationToken ct = ctSource.Token;

        IsRunning = true;

        SPLProgram program = new(Code, new() { ConsoleViewModel.Output }, ConsoleViewModel.Input);

        await program.Build(ct);
        await program.Execute(ct);

        IsRunning = false;
    }

    [RelayCommand]
    private void Cancel() => ctSource?.Cancel();

    [RelayCommand(CanExecute = nameof(ClearOutputCanExecute))]
    private void ClearOutput() => ConsoleViewModel.ClearOutput();

    private bool ClearOutputCanExecute() => !IsRunning;

    [RelayCommand]
    private async Task Close()
    {
        if (!IsSaved)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to save before closing?", "Closing", MessageBoxButton.YesNoCancel);

            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    await Save();
                    _close(this);
                    break;
                case MessageBoxResult.No:
                    _close(this);
                    break;
                default:
                    return;
            }
        }
        _close(this);
    }

    partial void OnIsSavedChanged(bool oldValue, bool newValue)
    {
        if (oldValue && !newValue)
        {
            Document.FileName += " *";
            OnPropertyChanged(nameof(FileName));
            return;
        }
        if (newValue)
        {
            Document.FileName = Document.FileName.TrimEnd(new char[] { '*' }).TrimEnd();
            OnPropertyChanged(nameof(FileName));
            return;
        }
    }
}
