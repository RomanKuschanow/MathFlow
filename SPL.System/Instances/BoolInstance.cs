using SPL.System.Types;

namespace SPL.System.Instances;
public class BoolInstance : IInstance<BoolType>
{
    public BoolType Type => BoolType.Instance;

    internal bool Value { get; set; }

    public BoolInstance(bool value = false)
    {
        Value = value;
    }
}
