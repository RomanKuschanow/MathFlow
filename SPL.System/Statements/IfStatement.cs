using SPL.System.Instances;
using SPL.System.Statements.Expressions;
using SPL.System.Types;

namespace SPL.System.Statements;
public class IfStatement : IStatement, IStatementList
{
    private readonly IExpression _condition;
    private readonly Action<LinkedList<IStatement>> _getStatements;
    private readonly ElsePart _else;

    public LinkedList<IStatement> Statements { get; init; }

    public IScope Parent { get; init; }

    public List<Variable> Variables { get; init; }

    public IfStatement(LinkedList<IStatement> statements, IScope parentScope, IExpression condition, Action<LinkedList<IStatement>> getStatements, ElsePart elsePart = null!)
    {
        Statements = statements ?? throw new ArgumentNullException(nameof(statements));
        Parent = parentScope ?? throw new ArgumentNullException(nameof(parentScope));
        Variables = new();
        _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        _getStatements = getStatements ?? throw new ArgumentNullException(nameof(getStatements));
        _else = elsePart ?? new ElsePart(new(), parentScope);
    }

    public List<Variable> GetAllVariablesInScope() => Variables.Concat(Parent.GetAllVariablesInScope()).ToList();

    public void Execute()
    {
        var condition = _condition.GetValue();

        if (condition.Type is not BoolType)
            throw new InvalidDataException($"condition must be a type of 'Bool', but found '{condition.Type.Name}'");

        if (((BoolInstance)condition).Value)
        {
            _getStatements(Statements);
            return;
        }
        else
        {
            _getStatements(_else.Statements);
            return;
        }
    }
}
