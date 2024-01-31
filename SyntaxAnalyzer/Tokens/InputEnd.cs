using SyntaxAnalyzer.Rules.Symbols;

namespace SyntaxAnalyzer.Tokens;
public record InputEnd : IToken
{
    public ISymbol Symbol => AcceptSymbol.Instance;

    public string Value => "";
}
