#nullable disable
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Threading;
using UI.Models;

namespace UI.ViewModels;

partial class ConsoleViewModel : ObservableObject
{
    public event Action<object> ItemAdded;

    [ObservableProperty]
    private ObservableCollection<ObservableObject> iOViewModels;

    private ObservableCollection<IConsoleIO> iO;

    private Dispatcher _dispatcher;

    public ConsoleViewModel(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        IOViewModels = new();
        IOViewModels.CollectionChanged += async (s, e) =>
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems.Count > 0)
            {
                await _dispatcher.InvokeAsync(() => ItemAdded?.Invoke(e.NewItems[^1]));
            }
        };
        iO = new();
        iO.CollectionChanged += IO_CollectionChanged;
    }

    private async void IO_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Reset)
        {
            IOViewModels.Clear();
            return;
        }

        var newObj = e.NewItems.Cast<IConsoleIO>().Last();

        if (newObj is ConsoleOutput)
        {
            await _dispatcher.InvokeAsync(() => IOViewModels.Add(new ConsoleOutputViewModel(newObj as ConsoleOutput)));
        }
        else if (newObj is ConsoleInput)
        {
            await _dispatcher.InvokeAsync(() => IOViewModels.Add(new ConsoleInputViewModel(newObj as ConsoleInput)));
        }
        else
            throw new InvalidDataException(nameof(newObj));
    }

    public Task Output(string str, CancellationToken ct)
    {
        iO.Insert(iO.Count, new ConsoleOutput(str));
        return Task.CompletedTask;
    }

    public async Task<string> Input(string str, CancellationToken ct)
    {
        ConsoleInput consoleInput = new(str);
        iO.Add(consoleInput);
        return await consoleInput.GetInput();
    }

    public void ClearOutput()
    {
        iO.Clear();
    }

    public void Cancel()
    {
        if (iO.Where(i => i is ConsoleInput).Count() > 0)
            ((ConsoleInput)iO.Last(i => i is ConsoleInput)).tcs.SetCanceled();
    }
}
