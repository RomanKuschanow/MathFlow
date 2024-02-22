using MathFlow.TypeSystem;

namespace MathFlow.SemanticAnalyzer.Scope;
public interface IScope
{
    public IScope? ParentScope { get; }
    public IMember GetMember(Guid id);
}
