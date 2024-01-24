using MathFlow.SemanticAnalyzer.Scope;
using MathFlow.TypeSystem.Instances;

namespace MathFlow.TypeSystem.Functions;
public interface IFunction : IMember
{
    public List<Variable> Arguments { get; }
    public Type Returns { get; }

    public IInstance Execute(IScope scope, params IInstance[] instances);
}
