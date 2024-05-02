using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI.Models;

class ConsoleInput : IConsoleIO
{
    public string Text { get; init; }
	public TaskCompletionSource<string> tcs { get; init; }

	public ConsoleInput(string text)
	{
		Text = text ?? throw new ArgumentNullException(nameof(text));
		tcs = new();
	}

	public Task<string> GetInput()
	{
		return tcs.Task;
	}
}
