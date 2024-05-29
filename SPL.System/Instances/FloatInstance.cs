using SPL.System.Types;
using System.Globalization;

namespace SPL.System.Instances;
public class FloatInstance : IInstance<FloatType>
{
    public FloatType Type => FloatType.Instance;

    internal double Value { get; set; }

    public FloatInstance(double value)
    {
        Value = value;
    }

    public override string ToString() => Value.ToString(new NumberFormatInfo());

    public static explicit operator IntInstance(FloatInstance floatInstance) => new(Convert.ToInt64(floatInstance.Value));
}
