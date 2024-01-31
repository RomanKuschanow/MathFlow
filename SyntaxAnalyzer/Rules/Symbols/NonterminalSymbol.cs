namespace SyntaxAnalyzer.Rules.Symbols;
public record NonterminalSymbol : ISymbol, IHaveValue<string>
{
    public SymbolType Type => SymbolType.Nonterminal;
    public string Value { get; init; }

    public NonterminalSymbol(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override string ToString() => $"{Value}";
}
