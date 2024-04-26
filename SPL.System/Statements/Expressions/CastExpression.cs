using SPL.System.Instances;
using SPL.System.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL.System.Statements.Expressions;
public class CastExpression : IExpression
{
    private readonly IType _to;
    private readonly IExpression _expression;

    public CastExpression(IType to, IExpression expression)
    {
        _to = to ?? throw new ArgumentNullException(nameof(to));
        _expression = expression ?? throw new ArgumentNullException(nameof(expression));
    }

    public IInstance<IType> GetValue() => _to.Cast(_expression.GetValue());
}
