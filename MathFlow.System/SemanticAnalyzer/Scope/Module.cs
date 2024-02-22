using MathFlow.SemanticAnalyzer.Statements;
using MathFlow.TypeSystem;
using System.Collections.Immutable;

namespace MathFlow.SemanticAnalyzer.Scope;
public class Module : IScope, IStatementList, IMember
{
    public Guid MemberId { get; } = Guid.NewGuid();

    public ImmutableList<IStatement> Statements { get; init; }

    public ImmutableList<Field> Fields { get; init; } 

    public IScope? ParentScope { get; init;}

    public Module(IEnumerable<IStatement> statements, IEnumerable<Field> fields, IScope? scope = null)
    {
        Statements = (statements ?? throw new ArgumentNullException(nameof(statements))).ToImmutableList();
        Fields = (fields ?? throw new ArgumentNullException(nameof(fields))).ToImmutableList();
        ParentScope = scope;
    }

    public IMember GetMember(Guid id) => Fields.Where(f => f.Visibility == Visibility.Public).Cast<IMember>().Single(m => m.MemberId == id);
}
