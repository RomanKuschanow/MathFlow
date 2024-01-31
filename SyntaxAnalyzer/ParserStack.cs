#nullable disable

using SyntaxAnalyzer;
using SyntaxAnalyzer.Generator;
using SyntaxAnalyzer.Generator.Actions;
using SyntaxAnalyzer.Rules;
using SyntaxAnalyzer.Rules.Symbols;
using SyntaxAnalyzer.Tokens;

namespace SyntaxAnalyzer;
public class ParserStack
{
    private Stack<IToken> _tokens = new();
    private Stack<State> _states = new();

    public ParserStack(State init)
    {
        _states.Push(init);
    }

    public void Shift(IToken token, ShiftAction shift)
    {
        _tokens.Push(token);
        _states.Push(shift.DestState);
    }

    public void Reduce(ReduceAction reduce)
    {
        List<IToken> tokens = new();

        foreach (var _ in reduce.Rule.Tokens)
        {
            tokens.Add(_tokens.Pop());
            _states.Pop();
        }

        tokens.Reverse();

        Nonterminal newToken = new(reduce.Rule.NonTerminal, tokens);
        _tokens.Push(newToken);
    }

    public void GoTo(State state)
    {
        _states.Push(state);
    }

    public Nonterminal Accept() => _tokens.Pop() as Nonterminal;

    public State GetState() => _states.Peek();
    public ISymbol GetSymbol() => _tokens.Peek().Symbol;
}
