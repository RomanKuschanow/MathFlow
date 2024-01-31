namespace SyntaxAnalyzer.Rules.Symbols;
public record TerminalSymbol : ISymbol, IHaveValue<string>
{
    public SymbolType Type => SymbolType.Terminal;
    public string Value { get; init; }

    public TerminalSymbol(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override string ToString() => $"{Value}";
}
