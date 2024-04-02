using SPL.System.Types;

namespace SPL.System.Instances;
public class StringInstance : IInstance<StringType>
{
    public StringType Type => StringType.Instance;

    internal string Value { get; set; }

    public StringInstance(string value)
    {
        Value = value;
    }
}
