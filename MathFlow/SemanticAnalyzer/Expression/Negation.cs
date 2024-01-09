using MathFlow.SemanticAnalyzer.Datatypes;

namespace MathFlow.SemanticAnalyzer.Expression;
public class Negation : IExpression
{
    private readonly IExpression _value;

    public Negation(IExpression value)
    {
        _value = value;
    }

    public Num GetValue() => -_value.GetValue();
}
