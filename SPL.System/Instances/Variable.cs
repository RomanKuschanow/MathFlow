#nullable disable
using SPL.System.Types;

namespace SPL.System.Instances;
public class Variable
{
    public string Name { get; init; }
    public IType Type { get; init; }
    public IInstance<IType> Instance { get; private set; }

    public Variable(string name, IType type, IInstance<IType> instance = null!)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        Name = name;
        Type = type ?? throw new ArgumentNullException(nameof(type));
        SetValue(instance ?? type.GetInstance());
    }

    public void SetValue(IInstance<IType> instance)
    {
        if (instance.Type == Type)
            Instance = instance;

        throw new InvalidDataException($"cannot assign type '{instance.Type}' to '{Type}'");
    }
}
