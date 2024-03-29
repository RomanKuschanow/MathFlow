using SPL.System.Types;

namespace SPL.System.Instances;
public class NumInstance : IInstance<NumType>
{
    public NumType Type => NumType.Instance;

    internal double _value;

    public NumInstance(double value)
    {
        _value = value;
    }
}
