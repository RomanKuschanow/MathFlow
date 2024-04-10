using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements;
public interface IScope
{
    IScope Parent { get; }
    List<Variable> Variables { get; }

    List<Variable> GetAllVariablesInScope();

    void CreateVariable(string name, IType type, IInstance<IType> value = null!)
    {
        if (GetAllVariablesInScope().Select(v => v.Name).Contains(name))
            throw new InvalidDataException($"value with name '{name}' already exists");

        Variables.Add(new Variable(name, type, value));
    }

    IInstance<IType> GetValue(string name)
    {
        var variable = GetAllVariablesInScope().SingleOrDefault(v => v.Name == name);

        if (variable is not null)
            return variable.Instance;

        throw new InvalidDataException($"variable named '{name}' does not exists");
    }

    void AssignVariable(string name, IInstance<IType> value)
    {
        var variable = GetAllVariablesInScope().SingleOrDefault(v => v.Name == name);

        if (variable is not null)
        {
            if (variable.Type == value.Type)
            {
                variable.SetValue(value);
                return;
            }

            throw new InvalidDataException($"cannot assign type '{value.Type.Name}' to '{variable.Type.Name}'");
        }

        throw new InvalidDataException($"variable named '{name}' does not exists");
    }
}
