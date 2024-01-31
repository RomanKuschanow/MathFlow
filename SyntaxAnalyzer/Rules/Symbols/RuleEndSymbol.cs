namespace SyntaxAnalyzer.Rules.Symbols;
public record RuleEndSymbol : ISymbol
{
    public SymbolType Type => SymbolType.RuleEnd;
}
