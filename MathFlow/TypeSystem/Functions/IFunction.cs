using MathFlow.TypeSystem.Instances;

namespace MathFlow.TypeSystem.Functions;
public interface IFunction
{
    public List<Variable> Arguments { get; }
    public Type Returns { get; }

    public IInstance Execute(params IInstance[] instances);
}
