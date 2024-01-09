using MathFlow.SemanticAnalyzer.Datatypes;
using MathFlow.SemanticAnalyzer.Expression;
using MathFlow.SemanticAnalyzer.Statements;
using MathFlow.SyntaxAnalyzer;
using System.Collections.Immutable;

namespace MathFlow.SemanticAnalyzer;
public class Analyzer
{
    public SemanticTree Analyze(NonTerminal syntaxTree)
    {
        List<string> variables = new();

        Stack<NonTerminal> stack = new();

        NonTerminal statementList = syntaxTree;

        while (true)
        {
            if (statementList.Tokens.Count > 1)
            {
                stack.Push((NonTerminal)((NonTerminal)statementList.Tokens.Last()).Tokens[0]);
                statementList = (NonTerminal)statementList.Tokens.First();
                continue;
            }
            else
            {
                stack.Push((NonTerminal)((NonTerminal)statementList.Tokens.First()).Tokens[0]);
                break;
            }
        }

        stack = new(stack.Reverse());

        List<IStatement> statements = new();

        SemanticTree semanticTree = new(statements);

        while (stack.Count > 0)
        {
            NonTerminal statement = stack.Pop();

            switch (statement.Name)
            {
                case "Declaration":
                    string type = ((Terminal)statement.Tokens[0]).Value.Value;
                    string declarationName = ((Terminal)statement.Tokens[1]).Value.Value;

                    if (variables.Contains(declarationName))
                    {
                        throw new Exception($"A local variable named '{declarationName}' is already defined in this scope");
                    }

                    Declaration declaration;

                    if (statement.Tokens.Count == 2)
                    {
                        declaration = new(semanticTree.Declare, type, declarationName);
                    }
                    else
                    {
                        IExpression declarationValue = GetExpression((NonTerminal)statement.Tokens.Last(), semanticTree.GetValue, variables.ToImmutableList());
                        declaration = new(semanticTree.Declare, type, declarationName, declarationValue);
                    }

                    statements.Add(declaration);
                    variables.Add(declarationName);
                    break;
                case "Assignment":
                    string AssignmentName = ((Terminal)statement.Tokens[0]).Value.Value;

                    if (!variables.Contains(AssignmentName))
                    {
                        throw new Exception($"The name '{AssignmentName}' does not exist in the current context");
                    }

                    IExpression AssignmentValue = GetExpression((NonTerminal)statement.Tokens.Last(), semanticTree.GetValue, variables.ToImmutableList());

                    statements.Add(new Assignment(semanticTree.Assign, AssignmentName, AssignmentValue));
                    break;
                case "PrintStatement":
                    IExpression printValue = GetExpression((NonTerminal)statement.Tokens.SkipLast(1).Last(), semanticTree.GetValue, variables.ToImmutableList());

                    statements.Add(new PrintStatement(semanticTree.Print, () => printValue.GetValue().ToString()));
                    break;
            }
        }

        semanticTree.Complete();

        return semanticTree;
    }

    private IExpression GetExpression(NonTerminal expression, Func<string, Num> getValue, ImmutableList<string> variables)
    {
        if (expression.Tokens.Count == 3)
        {
            if (((Terminal)expression.Tokens[1]).Value.Value == "+")
                return new Addition(
                    GetExpression((NonTerminal)expression.Tokens.First(), getValue, variables),
                    GetTerm((NonTerminal)expression.Tokens.Last(), getValue, variables));
            else
                return new Subtraction(
                    GetExpression((NonTerminal)expression.Tokens.First(), getValue, variables),
                    GetTerm((NonTerminal)expression.Tokens.Last(), getValue, variables));
        }
        else
            return GetTerm((NonTerminal)expression.Tokens.First(), getValue, variables);
    }

    private IExpression GetTerm(NonTerminal term, Func<string, Num> getValue, ImmutableList<string> variables)
    {
        if (term.Tokens.Count == 3)
        {
            if (((Terminal)term.Tokens[1]).Value.Value == "*")
                return new Multiplication(
                    GetTerm((NonTerminal)term.Tokens.First(), getValue, variables),
                    GetFactor((NonTerminal)term.Tokens.Last(), getValue, variables));
            else
                return new Division(
                    GetTerm((NonTerminal)term.Tokens.First(), getValue, variables),
                    GetFactor((NonTerminal)term.Tokens.Last(), getValue, variables));
        }
        else
            return GetFactor((NonTerminal)term.Tokens.First(), getValue, variables);
    }

    private IExpression GetFactor(NonTerminal factor, Func<string, Num> getValue, ImmutableList<string> variables)
    {
        if (factor.Tokens.Count == 3)
            return GetExpression((NonTerminal)factor.Tokens[1], getValue, variables);
        else
        {
            if (factor.Tokens.First() is NonTerminal terminal)
                return new Negation(GetFactor((NonTerminal)terminal.Tokens.Last(), getValue, variables));
            else
            {
                if (factor.Tokens.First().Name == "number")
                    return new Constant(GetNumber(((Terminal)factor.Tokens.First()).Value.Value));
                else
                {
                    string identifier = ((Terminal)factor.Tokens.First()).Value.Value;

                    if (!variables.Contains(identifier))
                    {
                        throw new Exception($"The name '{identifier}' does not exist in the current context");
                    }
                    return new Identifier(identifier, getValue);
                }
            }
        }
    }

    private Num GetNumber(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            throw new ArgumentException($"'{nameof(str)}' cannot be null or whitespace.", nameof(str));
        }

        if (str.Last() == 'm')
            return new Num(Convert.ToDecimal(str[..^1]));
        else
            return new Num(Convert.ToDouble(str));
    }
}
