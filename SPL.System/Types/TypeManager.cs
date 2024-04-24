using System.Collections.Immutable;

namespace SPL.System.Types;
public class TypeManager
{
    ImmutableList<IType> Types { get; init; }

    public TypeManager(IEnumerable<IType> types)
    {
        if (types is null)
        {
            throw new ArgumentNullException(nameof(types));
        }

        Types = ImmutableList.CreateRange(types);
    }

    public IType? GetTypeByAlias(string name)
    {
        return Types.SingleOrDefault(t => t.Aliases.Contains(name));
    }
}
