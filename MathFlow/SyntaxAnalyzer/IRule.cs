using System.Collections.Immutable;

namespace MathFlow.SyntaxAnalyzer;

public interface IRule
{
    public string NonTerminal { get; }

    public ImmutableList<string> Tokens { get; }
}