using SPL.System.Types;

namespace SPL.System.Instances;
public class BoolInstance : IInstance<BoolType>
{
    public BoolType Type => BoolType.Instance;

    internal bool _value;

    public BoolInstance(bool value = false)
    {
        _value = value;
    }
}
