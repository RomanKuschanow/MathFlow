using SPL.System.Instances;
using SPL.System.Statements.Expressions;
using SPL.System.Types;

namespace SPL.System.Statements;
public class Declaration : IStatement
{
    private readonly Action<string, IType, IInstance<IType>?> _declareValue;
    private readonly string _name;
    private readonly IType _type;
    private readonly IExpression _value;

    public Declaration(Action<string, IType, IInstance<IType>?> declareValue, string name, IType type, IExpression value = null!)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        _declareValue = declareValue ?? throw new ArgumentNullException(nameof(declareValue));
        _name = name;
        _type = type ?? throw new ArgumentNullException(nameof(type));
        _value = value ?? new InstanceExpression(_type.GetInstance());
    }

    public async Task Execute(CancellationToken ct) => await Task.Run(() => _declareValue(_name, _type, _value.GetValue()), ct);
}
