using MathFlow.SemanticAnalyzer.Expression;

namespace MathFlow.SemanticAnalyzer.Statements;
public class Assignment : IStatement
{
    private readonly string _name;
    private readonly IExpression _value;

    private readonly Action<string, IExpression> _assign;

    public Assignment(Action<string, IExpression> assign, string name, IExpression value)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        _assign = assign ?? throw new ArgumentNullException(nameof(assign));
        _name = name;
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public void Execute() => _assign(_name, _value);
}
