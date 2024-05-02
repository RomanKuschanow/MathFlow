using UI.ViewModels;

namespace UI.Models;

class ConsoleOutput : IConsoleIO
{
    public string Text { get; init; }

    public ConsoleOutput(string text)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }
}
