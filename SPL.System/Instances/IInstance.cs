using SPL.System.Types;

namespace SPL.System.Instances;
public interface IInstance<out T> where T : IType
{
    T Type { get; }
}
