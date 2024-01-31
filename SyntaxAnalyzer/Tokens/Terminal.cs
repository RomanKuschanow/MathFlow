using SyntaxAnalyzer.Rules.Symbols;

namespace SyntaxAnalyzer.Tokens;
public record Terminal : IToken
{
    public ISymbol Symbol { get; set; }

    public string Value { get; init; }

    public Terminal(ISymbol symbol, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
        }

        Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
        Value = value;
    }
}
