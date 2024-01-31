using SyntaxAnalyzer.Rules.Symbols;

namespace SyntaxAnalyzer.Generator.Actions;
public record ShiftAction : IAction
{
    public ActionType Type => ActionType.Shift;

    public State DestState { get; init; }

    public State InitState { get; init; }

    public ISymbol Symbol { get; init; }

    public ShiftAction(State destState, State initState, ISymbol symbol)
    {
        DestState = destState;
        InitState = initState;
        Symbol = symbol;
    }

    public override string ToString() => $"Shift [{InitState}] [{Symbol}] => {DestState}";

    public virtual bool Equals(ShiftAction action)
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
