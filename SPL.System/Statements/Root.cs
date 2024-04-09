using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements;
public class Root : IScope
{
    public IScope Parent => null!;

    public Dictionary<string, IInstance<IType>> Values { get; init; }

    public Root()
    {
        Values = new();
    }
}
