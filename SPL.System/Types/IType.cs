using SPL.System.Instances;
using SPL.System.Operators;
using System.Collections.Immutable;

namespace SPL.System.Types;
public interface IType
{
    string Name { get; }

    ImmutableList<string> Aliases { get; }

    IEnumerable<IOperator> Operators { get; }

    IInstance<IType> GetInstance(params object[] args);

    bool IsInstance(IInstance<IType> instance);
}
