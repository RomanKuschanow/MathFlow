using SPL.System.Types;

namespace SPL.System.Instances;
public class NumInstance : IInstance<NumType>
{
    public NumType Type => NumType.Instance;

    internal double Value { get; set; }

    public NumInstance(double value)
    {
        Value = value;
    }
}
