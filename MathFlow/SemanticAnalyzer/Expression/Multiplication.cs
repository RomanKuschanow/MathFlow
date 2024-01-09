using MathFlow.SemanticAnalyzer.Datatypes;

namespace MathFlow.SemanticAnalyzer.Expression;
public class Multiplication : IExpression
{
    private readonly IExpression _a;
    private readonly IExpression _b;

    public Multiplication(IExpression a, IExpression b)
    {
        _a = a;
        _b = b;
    }

    public Num GetValue() => _a.GetValue() * _b.GetValue();
}
