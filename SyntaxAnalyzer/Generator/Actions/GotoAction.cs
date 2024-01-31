using SyntaxAnalyzer.Rules.Symbols;

namespace SyntaxAnalyzer.Generator.Actions;
public record GotoAction : IAction
{
    public ActionType Type => ActionType.Goto;

    public State DestState { get; set; }

    public State InitState { get; init; }

    public ISymbol Symbol { get; init; }

    public GotoAction(State destState, State initState, ISymbol symbol)
    {
        DestState = destState;
        InitState = initState;
        Symbol = symbol;
    }

    public override string ToString() => $"Goto [{InitState}] [{Symbol}] => {DestState}";

    public virtual bool Equals(GotoAction action)
    {
        if (action is null)
        {
            return false;
        }

        if (DestState != action.DestState)
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
