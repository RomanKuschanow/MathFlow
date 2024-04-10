using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements;
public class Root : IScope
{
    public IScope Parent => null!;

    public List<Variable> Variables { get; init; }

    public Root()
    {
        Variables = new();
    }

    public List<Variable> GetAllVariablesInScope() => Variables;
}
