using SyntaxAnalyzer.Rules.Symbols;
using System.Collections.Immutable;

namespace SyntaxAnalyzer.Tokens;
public record Nonterminal : IToken
{
    public ISymbol Symbol { get; init; }

    public ImmutableArray<IToken> Tokens { get; init; }

    public Nonterminal(ISymbol symbol, IEnumerable<IToken> tokens)
    {
        Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
        Tokens = (tokens ?? throw new ArgumentNullException(nameof(tokens))).ToImmutableArray();
    }
}
