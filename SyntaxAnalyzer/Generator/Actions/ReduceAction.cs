using SyntaxAnalyzer.Rules;
using SyntaxAnalyzer.Rules.Symbols;

namespace SyntaxAnalyzer.Generator.Actions;
public record ReduceAction : IAction
{
    public ActionType Type => ActionType.Reduce;

    public IRule Rule { get; init; }

    public State InitState { get; init; }

    public ISymbol Symbol { get; init; }

    public ReduceAction(IRule rule, State initState, ISymbol symbol)
    {
        Rule = rule;
        InitState = initState;
        Symbol = symbol;
    }

    public override string ToString() => $"Reduce [{InitState}] [{Symbol}] => {Rule}";

    public virtual bool Equals(ReduceAction action)
    {
        if (action is null)
        {
            return false;
        }

        if (Rule != action.Rule)
        {
            return false;
        }

        if (InitState != action.InitState)
        {
            return false;
        }

        if (Symbol != action.Symbol)
        {
            return false;
        }

        return true;
    }
}
