using SPL.System.Types;

namespace SPL.System.Instances;
public class IntInstance : IInstance<IntType>
{
    public IntType Type => IntType.Instance;

    internal long Value { get; set; }

    public IntInstance(long value)
    {
        Value = value;
    }

    public override string ToString() => Value.ToString();

    public static explicit operator FloatInstance(IntInstance intInstance) => new(Convert.ToDouble(intInstance.Value));
}
