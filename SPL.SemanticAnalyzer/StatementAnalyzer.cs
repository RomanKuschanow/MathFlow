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
        throw new NotImplementedException();
    }

    private IfStatement GetIfStatement(Nonterminal ifStatement, Program program, IStatementList statementList)
    {
        throw new NotImplementedException();
    }

    private WhileStatement GetWhileStatement(Nonterminal whileStatement, Program program, IStatementList statementList)
    {
        throw new NotImplementedException();
    }
}
