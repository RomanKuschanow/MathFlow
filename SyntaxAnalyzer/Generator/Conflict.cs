using SyntaxAnalyzer.Generator.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxAnalyzer.Generator;
public record Conflict
{
    public IAction First { get; init; }
    public IAction Second { get; init; }

    public Conflict(IAction first, IAction second)
    {
        First = first.Type == ActionType.Goto ? throw new ArgumentException("Action must be 'Shift' or 'Rule'", nameof(first)) : first;
        Second = second.Type == ActionType.Goto ? throw new ArgumentException("Action must be 'Shift' or 'Rule'", nameof(second)) : second;
    }
}
