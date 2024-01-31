namespace SyntaxAnalyzer.Rules.Symbols;
public record VoidSymbol : TerminalSymbol
{
    public new SymbolType Type => SymbolType.Void;

    public VoidSymbol() : base("")
    {
    }
}
