using SyntaxAnalyzer.Rules.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxAnalyzer.Generator.Actions;
public record AcceptAction : IAction
{
    public ActionType Type => ActionType.Accept;

    public State InitState { get; init; }

    public ISymbol Symbol => AcceptSymbol.Instance;

    public AcceptAction(State initState)
    {
        InitState = initState ?? throw new ArgumentNullException(nameof(initState));
    }

    public override string ToString() => $"Accept [{InitState}] [$]";
}
