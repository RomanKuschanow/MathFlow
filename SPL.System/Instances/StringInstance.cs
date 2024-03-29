using SPL.System.Types;

namespace SPL.System.Instances;
public class StringInstance : IInstance<StringType>
{
    public StringType Type => StringType.Instance;

    internal string _value;

    public StringInstance(string value)
    {
        _value = value;
    }
}
