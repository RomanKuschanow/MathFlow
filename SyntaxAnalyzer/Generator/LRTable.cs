using SyntaxAnalyzer.Generator.Actions;
using SyntaxAnalyzer.Rules;
using SyntaxAnalyzer.Rules.Symbols;
using System.Collections.Immutable;
using System.Linq;

namespace SyntaxAnalyzer.Generator;
public record LRTable
{
    public ImmutableArray<IAction> Actions { get; private set; }
    public ImmutableArray<Conflict> Conflicts { get; init; }
    public State InitState { get; init; }

    public LRTable(
        IEnumerable<IAction> actions,
        State initState,
        IEnumerable<Conflict>? conflicts = null)
    {
        Actions = (actions ?? throw new ArgumentNullException(nameof(actions))).ToImmutableArray();
        Conflicts = (conflicts ?? new List<Conflict>()).ToImmutableArray();
        InitState = initState;
    }

    public List<KeyValuePair<int, List<KeyValuePair<TerminalSymbol, int[]>>>> GetConflicts() => Conflicts.Cast<KeyValuePair<int, List<KeyValuePair<TerminalSymbol, int[]>>>>().ToList();

    public void SolveConflict(IAction conflict, IAction solve)
    {
        if (conflict is null)
        {
            throw new ArgumentNullException(nameof(conflict));
        }
        if (solve is null)
        {
            throw new ArgumentNullException(nameof(solve));
        }

        if (!Actions.Contains(conflict) || !Conflicts.SelectMany(c => new IAction[] { c.First, c.Second }).Contains(conflict))
        {
            throw new ArgumentException("Conflict action must contain in 'Actions' and in 'Conflicts'", nameof(conflict));
        }
        if (!Conflicts.SelectMany(c => new IAction[] { c.First, c.Second }).Contains(solve))
        {
            throw new ArgumentException("Solve action must contain in 'Conflicts'", nameof(conflict));
        }

        _ = Conflicts.Single(c => (c.First == conflict && c.Second == solve) || (c.First == solve && c.Second == conflict));

        Actions = Actions.Remove(conflict);
        Actions = Actions.Add(solve);
    }
}
