#nullable disable
using SPL.System;
using SPL.System.Statements;
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

        LinkedList<IStatement> rootStatements = new();
        Root root = new(rootStatements);
        var rawStatements = GetStatements(syntaxTree);

        Program program = new(root);

        ProcessScopes(program, root, rootStatements, rawStatements);

        return program;
    }

    private List<Nonterminal> GetStatements(Nonterminal statementList)
    {
        if (statementList is null)
        {
            throw new ArgumentNullException(nameof(statementList));
        }

        if (statementList.SymbolName != "StatementList")
        {
            throw new InvalidDataException(nameof(statementList));
        }

        List<Nonterminal> result = new();

        var currentItem = statementList;

        while (currentItem.SymbolName == "StatementList")
        {
            result.Add(currentItem.Tokens.Last() as Nonterminal);
            currentItem = currentItem.Tokens[0] as Nonterminal;
        }

        return result;
    }

    private void ProcessScopes(Program program, IStatementList statementList, LinkedList<IStatement> statements, List<Nonterminal> rawStatements)
    {
        throw new NotImplementedException();
    }
}
