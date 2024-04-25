#nullable disable
using SPL.System;
using SPL.System.Statements;
using SPL.System.Types;
using SyntaxAnalyzer.Tokens;
using System.Xml.Linq;

namespace SPL.SemanticAnalyzer;
public partial class Analyzer
{
    private Declaration GetDeclaration(Nonterminal declaration, Program program, IStatementList statementList)
    {
        if (declaration.SymbolName != "Declaration")
        {
            throw new InvalidDataException(nameof(declaration));
        }

        var typeName = (declaration.Tokens[0] as Terminal).Value;
        var varName = (declaration.Tokens[1] as Terminal).Value;

        IType type = program.TypeManager.GetTypeByAlias(typeName);


        if (declaration.Tokens.Length == 2)
        {
            return new Declaration(statementList.CreateVariable, varName, type);
        }
        else if (declaration.Tokens.Length == 4)
        {
            return new Declaration(statementList.CreateVariable, varName, type, GetExpression(declaration.Tokens.Last() as Nonterminal, program, statementList));
        }
        else
        {
            throw new InvalidDataException(nameof(declaration));
        }
    }

    private Assignment GetAssignment(Nonterminal assignment, Program program, IStatementList statementList)
    {
        if (assignment.SymbolName != "Assignment")
        {
            throw new InvalidDataException(nameof(assignment));
        }

        var varName = (assignment.Tokens[0] as Terminal).Value;

        if (assignment.Tokens.Length == 3)
        {
            return new Assignment(statementList.AssignVariable, varName, GetExpression(assignment, program, statementList));
        }
        else
        {
            throw new InvalidDataException(nameof(assignment));
        }
    }

    private PrintStatement GetPrintStatement(Nonterminal printStatement, Program program, IStatementList statementList)
    {
        if (printStatement.SymbolName != "PrintStatement")
        {
            throw new InvalidDataException(nameof(printStatement));
        }

        var rawArgs = GetRawNonterminalList(printStatement.Tokens[2] as Nonterminal);

        var args = rawArgs.Select(a => GetExpression(a, program, statementList)).ToList();

        return new(program.ConsoleOut, args);
    }

    private IfStatement GetIfStatement(Nonterminal ifStatement, Program program, IStatementList statementList)
    {
        if (ifStatement.SymbolName != "IfStatement")
        {
            throw new InvalidDataException(nameof(ifStatement));
        }

        var condition = GetExpression(ifStatement.Tokens[2] as Nonterminal, program, statementList);
        var rawBlock = GetBlock(ifStatement.Tokens[4] as Nonterminal, program, statementList);
        LinkedList<IStatement> block = new();
        ElsePart elsePart = null;

        if ((ifStatement.Tokens[5] as Nonterminal).Tokens.Length > 0)
        {
            var rawElseBlock = GetBlock((ifStatement.Tokens[5] as Nonterminal).Tokens[1] as Nonterminal, program, statementList);
            LinkedList<IStatement> elseBlock = new();
            elsePart = new(elseBlock, statementList);

            ProcessScopes(program, elsePart, elseBlock, rawElseBlock);
        }

        IfStatement result = new(block, statementList, condition, program.PushToStack, elsePart);
        ProcessScopes(program, result, block, rawBlock);

        return result;
    }

    private WhileStatement GetWhileStatement(Nonterminal whileStatement, Program program, IStatementList statementList)
    {
        if (whileStatement.SymbolName != "WhileStatement")
        {
            throw new InvalidDataException(nameof(whileStatement));
        }

        var condition = GetExpression(whileStatement.Tokens[2] as Nonterminal, program, statementList);
        var rawBlock = GetBlock(whileStatement.Tokens[4] as Nonterminal, program, statementList);
        LinkedList<IStatement> block = new();

        WhileStatement result = new(block, statementList, condition, program.PushToStack);
        ProcessScopes(program, result, block, rawBlock);

        return result;
    }

    private List<Nonterminal> GetBlock(Nonterminal block, Program program, IStatementList statementList)
    {
        if (block.SymbolName != "Block")
        {
            throw new InvalidDataException(nameof(block));
        }

        if (block.Tokens.Length > 1)
        {
            return GetRawNonterminalList(block.Tokens[1] as Nonterminal);
        }
        else
        {
            return new() { block.Tokens[0] as Nonterminal };
        }
    }
}
