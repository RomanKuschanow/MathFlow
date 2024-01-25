using MathFlow.SemanticAnalyzer.Scope;
using MathFlow.TypeSystem.Instances;
using System.Collections.Immutable;

namespace MathFlow.TypeSystem.Functions;
public interface IFunction : IMember
{
    public ImmutableList<Variable> Arguments { get; }
    public Type Returns { get; }

    public IInstance Execute(IScope scope, params IInstance[] instances);
}
