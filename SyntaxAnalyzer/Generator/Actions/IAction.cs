using SyntaxAnalyzer.Rules.Symbols;

namespace SyntaxAnalyzer.Generator.Actions;
public interface IAction
{
    public ActionType Type { get; }
    public State InitState { get; }
    public ISymbol Symbol { get; }
}
