using SyntaxAnalyzer.Rules.Symbols;

namespace SyntaxAnalyzer.Generator;
public record Goto
{
    public State InitState { get; init; }
    public ISymbol Symbol { get; init; }
    public State ToState { get; init; }

    public Goto(State initState, ISymbol symbol, State toState)
    {
        InitState = initState;
        Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
        ToState = toState;
    }

    public override string ToString() => $"goto({InitState}, {Symbol}) => {ToState}";
}
