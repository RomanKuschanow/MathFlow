using SPL.System.Instances;
using SPL.System.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL.System.Statements.Expressions;
public class InstanceExpression : IExpression
{
    private IInstance<IType> _instance;

    public InstanceExpression(IInstance<IType> instance)
    {
        _instance = instance;
    }

    public IInstance<IType> GetValue() => _instance;
}
