using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Models;

namespace UI.ViewModels;

partial class ConsoleInputViewModel : ObservableObject
{
    private ConsoleInput _input;

    public string Text
    {
        get => _input.Text;
    }

    [ObservableProperty]
    private string inputText = "";

    public ConsoleInputViewModel(ConsoleInput consoleInput)
    {
        _input = consoleInput ?? throw new ArgumentNullException(nameof(consoleInput));
    }

    [RelayCommand]
    private void GetInput()
    {
        try
        {
            _input.tcs.SetResult(InputText);
        }
        catch
        {
        }
    }
}
