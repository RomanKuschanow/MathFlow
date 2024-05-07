using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using SPL;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace UI.ViewModels;

partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ExecutableInstanceViewModel> executableInstanceViewModels = new();

    private Dispatcher _dispatcher;

    public MainViewModel(Dispatcher dispatcher)
    {
        SPLProgram.UpdateParser();
        _dispatcher = dispatcher;
    }

    [RelayCommand]
    private void OpenFile()
    {
        OpenFileDialog openFileDialog = new()
        {
            Title = "Select File",
            Filter = "All files (*.*)|*.*"
        };

        var result = openFileDialog.ShowDialog();

        if (result.HasValue && result.Value)
            ExecutableInstanceViewModels.Add(new(_dispatcher, openFileDialog.FileName, CloseFile, true));
    }

    [RelayCommand]
    private void NewFile()
    {
        ExecutableInstanceViewModels.Add(new(_dispatcher, "", CloseFile, false));
    }

    private void CloseFile(ExecutableInstanceViewModel file) => ExecutableInstanceViewModels.Remove(file);
}
