using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements;
public interface IScope
{
    IScope Parent { get; }
    Dictionary<string, IInstance<IType>> Values { get; }

    bool CreateValue(string name, IInstance<IType> value) => Values.TryAdd(name, value);

    IInstance<IType> GetValue(string name)
    {
        if (Values.TryGetValue(name, out var value))
            return value;

        throw new InvalidDataException(nameof(name));
    }

    //TODO: SetValue
}
