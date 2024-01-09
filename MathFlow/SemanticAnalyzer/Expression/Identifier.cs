using MathFlow.SemanticAnalyzer.Datatypes;

namespace MathFlow.SemanticAnalyzer.Expression;
public class Identifier : IExpression
{
    private readonly string _name;

    private readonly Func<string, Num> _getValue;

    public Identifier(string name, Func<string, Num> getValue)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        _name = name;
        _getValue = getValue ?? throw new ArgumentNullException(nameof(getValue));
    }

    public Num GetValue() => _getValue(_name);
}
