using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
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
        _dispatcher = dispatcher;
    }

    [RelayCommand]
    private void OpenFile()
    {
        OpenFileDialog openFileDialog = new();
        openFileDialog.Title = "Select File";
        openFileDialog.Filter = "All files (*.*)|*.*";

        var result = openFileDialog.ShowDialog();

        if (result.HasValue && result.Value)
            ExecutableInstanceViewModels.Add(new(_dispatcher, openFileDialog.FileName, CloseFile));
    }

    private void CloseFile(ExecutableInstanceViewModel file)
    {
        ExecutableInstanceViewModels.Remove(file);
    }
}
