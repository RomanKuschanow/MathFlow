#nullable disable

namespace SyntaxAnalyzer.Rules.Symbols;
public record AcceptSymbol : ISymbol
{
    private static AcceptSymbol _instance;

    public static AcceptSymbol Instance => _instance ??= new AcceptSymbol();

    private AcceptSymbol() { }

    public SymbolType Type => SymbolType.Accept;

    public override string ToString() => "$";
}
