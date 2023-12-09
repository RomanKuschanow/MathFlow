#nullable disable

namespace MathFlow.SyntaxAnalyzer;
public class ParserStack
{
    private Stack<IToken> _tokens = new();
    private Stack<int> _states = new();

    public ParserStack()
    {
        _states.Push(0);
    }

    public void Shift(IToken token, int state)
    {
        _tokens.Push(token);
        _states.Push(state);
    }

    public void Reduce(IRule rule)
    {
        List<IToken> tokens = new();

        foreach (var _ in rule.Tokens)
        {
            tokens.Add(_tokens.Pop());
            _states.Pop();
        }

        NonTerminal newToken = new(rule.NonTerminal, tokens);
        _tokens.Push(newToken);
    }

    public void GoTo(int state)
    {
        _states.Push(state);
    }

    public NonTerminal Accept() => _tokens.Pop() as NonTerminal;

    public int GetState() => _states.Peek();
}
