#nullable disable
using SPL.System;
using SPL.System.Operators;
using SPL.System.Statements;
using SPL.System.Types;
using System.Reflection;
using SyntaxAnalyzer.Tokens;

namespace SPL.SemanticAnalyzer;
public partial class Analyzer
{
    public Program Analyze(Nonterminal syntaxTree)
    {
        if (syntaxTree is null)
        {
            throw new ArgumentNullException(nameof(syntaxTree));
        }

        List<IType> types = typeof(IType).Assembly.GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(IType)))
            .Select(t => t.GetProperty("Instance", BindingFlags.Static).GetValue(null) as IType)
            .ToList();

        List<IOperator> operators = types.Select(t => t.GetType())
            .Select(t => t.GetProperty("Operators").GetValue(t.GetProperty("Instance", BindingFlags.Static).GetValue(null)) as IEnumerable<IOperator>)
            .SelectMany(ol => ol)
            .ToList();

        TypeManager typeManager = new(types);
        OperatorsManager operatorsManager = new(operators);

        LinkedList<IStatement> rootStatements = new();
        Root root = new(rootStatements);
        var rawStatements = GetRawNonterminalList(syntaxTree);

        Program program = new(root, typeManager, operatorsManager);

        ProcessScopes(program, root, rootStatements, rawStatements);

        return program;
    }

    private List<Nonterminal> GetRawNonterminalList(Nonterminal list)
    {
        if (list is null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        List<Nonterminal> result = new();

        var currentItem = list;

        while (currentItem != result.Last())
        {
            result.Add(currentItem.Tokens.Last() as Nonterminal);
            currentItem = currentItem.Tokens[0] as Nonterminal;
        }

        return result;
    }

    private void ProcessScopes(Program program, IStatementList statementList, LinkedList<IStatement> statements, List<Nonterminal> rawStatements)
    {
        var statementsAnalyzers = GetType().GetMethods()
            .Where(m => m.Name.StartsWith("Get"))
            .ToDictionary<MethodInfo, string, Func<object, object[], object>>(k => k.Name[3..], v => v.Invoke);

        foreach (var rawStatement in rawStatements)
        {
            var statement = rawStatement.Tokens.First() as Nonterminal;

            statements.AddLast(statementsAnalyzers[statement.SymbolName](this, new object[] { statement, program, statementList }) as IStatement);
        }
    }
}
