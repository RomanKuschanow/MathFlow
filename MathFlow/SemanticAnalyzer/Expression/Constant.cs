using MathFlow.SemanticAnalyzer.Datatypes;

namespace MathFlow.SemanticAnalyzer.Expression;
public class Constant : IExpression
{
    private readonly Num _value;

    public Constant(Num value)
    {
        _value = value;
    }

    public Num GetValue() => _value;
}
