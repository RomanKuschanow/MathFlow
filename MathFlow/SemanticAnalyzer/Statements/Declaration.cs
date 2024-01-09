using MathFlow.SemanticAnalyzer.Expression;

namespace MathFlow.SemanticAnalyzer.Statements;
public class Declaration : IStatement
{
    private readonly string _type;
    private readonly string _name;
    private readonly IExpression _value;

    private readonly Action<string, string, IExpression> _declare;

    public Declaration(Action<string, string, IExpression> declare, string type, string name, IExpression value)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new ArgumentException($"'{nameof(type)}' cannot be null or whitespace.", nameof(type));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        _declare = declare ?? throw new ArgumentNullException(nameof(declare));
        _type = type;
        _name = name;
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Declaration(Action<string, string, IExpression> declare, string type, string name) : this(declare, type, name, new Constant(new Datatypes.Num())) { }

    public void Execute() => _declare(_type, _name, _value);
}
