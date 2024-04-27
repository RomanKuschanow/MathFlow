using SPL.System.Instances;
using SPL.System.Statements.Expressions;
using SPL.System.Types;
using System.Collections.Immutable;
using System.Linq;

namespace SPL.System.Statements;
public class WhileStatement : IStatement, IStatementList
{
    private readonly IExpression _condition;
    private readonly Action<LinkedList<IStatement>> _getStatements;
    private LinkedList<IStatement> _statements;

    public ImmutableList<IStatement> Statements => ImmutableList.CreateRange(_statements);

    public IScope Parent { get; init; }

    public List<Variable> Variables { get; init; }

    public WhileStatement(LinkedList<IStatement> statements, IScope parentScope, IExpression condition, Action<LinkedList<IStatement>> getStatements)
    {
        _statements = statements ?? throw new ArgumentNullException(nameof(statements));
        Parent = parentScope ?? throw new ArgumentNullException(nameof(parentScope));
        Variables = new();
        _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        _getStatements = getStatements ?? throw new ArgumentNullException(nameof(getStatements));
    }

    public List<Variable> GetAllVariablesInScope() => Variables.Concat(Parent.GetAllVariablesInScope()).ToList();

    public void Execute()
    {
        var condition = _condition.GetValue();

        Variables.Clear();

        if (condition.Type is not BoolType)
            throw new InvalidDataException($"condition must be a type of 'Bool', but found '{condition.Type.Name}'");

        if (((BoolInstance)condition).Value)
        {
            _getStatements(new LinkedList<IStatement>(new IStatement[] { this }));
            _getStatements(_statements);
            return;
        }
    }
}
