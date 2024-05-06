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
        IOViewModels.CollectionChanged += (s, e) =>
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems.Count > 0)
            {
                _dispatcher.BeginInvoke(() => ItemAdded?.Invoke(e.NewItems[^1]));
            }
        };
        iO = new();
        iO.CollectionChanged += IO_CollectionChanged;
    }

    private void IO_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Reset)
        {
            IOViewModels.Clear();
            return;
        }

        var newObj = e.NewItems.Cast<IConsoleIO>().Last();

        if (newObj is ConsoleOutput)
        {
            _dispatcher.BeginInvoke(() => IOViewModels.Insert(IOViewModels.Count, new ConsoleOutputViewModel(newObj as ConsoleOutput)));
        }
        else if (newObj is ConsoleInput)
        {
            _dispatcher.BeginInvoke(() => IOViewModels.Insert(IOViewModels.Count, new ConsoleInputViewModel(newObj as ConsoleInput)));
        }
        else
            throw new InvalidDataException(nameof(newObj));
    }

    public void Output(string str)
    {
        iO.Insert(iO.Count, new ConsoleOutput(str));
    }

    public async Task<string> Input(string str, CancellationToken ct)
    {
        ConsoleInput consoleInput = new(str);
        iO.Insert(iO.Count, consoleInput);
        return await consoleInput.GetInput();
    }

    public void ClearOutput()
    {
        iO.Clear();
    }
}
