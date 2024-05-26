using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements.Expressions;
public interface IExpression
{
    Task<IInstance<IType>> GetValue(CancellationToken ct);
}
