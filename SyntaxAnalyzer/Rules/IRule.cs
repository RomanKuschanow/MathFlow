using SyntaxAnalyzer.Rules.Symbols;
using System.Collections.Immutable;

namespace SyntaxAnalyzer.Rules;

public interface IRule
{
    public NonterminalSymbol NonTerminal { get; }

    public ImmutableArray<ISymbol> Tokens { get; }
}