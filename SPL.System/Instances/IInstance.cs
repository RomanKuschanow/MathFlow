using SPL.System.Types;

namespace SPL.System.Instances;
public interface IInstance<T> where T : IType
{
    T Type { get; }
}
