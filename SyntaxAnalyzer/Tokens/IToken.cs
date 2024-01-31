using SyntaxAnalyzer.Rules.Symbols;

namespace SyntaxAnalyzer.Tokens;

public interface IToken
{
    ISymbol Symbol { get; }
}
