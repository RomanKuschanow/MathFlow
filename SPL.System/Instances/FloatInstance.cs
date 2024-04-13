using SPL.System.Types;

namespace SPL.System.Instances;
public class FloatInstance : IInstance<FloatType>
{
    public FloatType Type => FloatType.Instance;

    internal double Value { get; set; }

    public FloatInstance(double value)
    {
        Value = value;
    }

    public override string ToString() => Value.ToString();
}
