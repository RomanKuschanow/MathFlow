#nullable disable
using SPL.System.Statements;
using SPL.System;
using SPL.System.Statements.Expressions;
using SyntaxAnalyzer.Tokens;
using System.Xml.Linq;
using SPL.System.Operators;
using System.Linq.Expressions;
using SyntaxAnalyzer.Rules.Symbols;
using SPL.System.Instances;
using System.Runtime.CompilerServices;
using SPL.System.Types;
using System.Numerics;

namespace SPL.SemanticAnalyzer;
public partial class Analyzer
{
    private IExpression GetExpression(Nonterminal expression, Program program, IStatementList statementList)
    {
        if (expression.SymbolName != "Expression")
        {
            throw new InvalidDataException(nameof(expression));
        }

        var and = GetAnd(expression.Tokens.Last() as Nonterminal, program, statementList);

        if (expression.Tokens.Length == 3)
        {
            return new OperatorExpression(new List<IExpression>() { GetExpression(expression.Tokens[0] as Nonterminal, program, statementList), and }, OperatorType.Or, program.OperatorsManager.GetOperator);
        }
        else
        {
            return and;
        }
    }

    private IExpression GetAnd(Nonterminal and, Program program, IStatementList statementList)
    {
        if (and.SymbolName != "And")
        {
            throw new InvalidDataException(nameof(and));
        }

        var equality = GetEquality(and.Tokens.Last() as Nonterminal, program, statementList);

        if (and.Tokens.Length == 3)
        {
            return new OperatorExpression(new List<IExpression>() { GetAnd(and.Tokens[0] as Nonterminal, program, statementList), equality }, OperatorType.And, program.OperatorsManager.GetOperator);
        }
        else
        {
            return equality;
        }
    }

    private IExpression GetEquality(Nonterminal equality, Program program, IStatementList statementList)
    {
        if (equality.SymbolName != "Equality")
        {
            throw new InvalidDataException(nameof(equality));
        }

        var relational = GetRelational(equality.Tokens.Last() as Nonterminal, program, statementList);

        if (equality.Tokens.Length == 3)
        {
            var _operator = (equality.Tokens[1].Symbol as IHaveValue<string>).Value switch
            {
                "==" => OperatorType.Equal,
                "!=" => OperatorType.NotEqual,
                _ => throw new()
            };

            return new OperatorExpression(new List<IExpression>() { GetEquality(equality.Tokens[0] as Nonterminal, program, statementList), relational }, _operator, program.OperatorsManager.GetOperator);
        }
        else
        {
            return relational;
        }
    }

    private IExpression GetRelational(Nonterminal relational, Program program, IStatementList statementList)
    {
        if (relational.SymbolName != "Relational")
        {
            throw new InvalidDataException(nameof(relational));
        }

        var additive = GetAdditive(relational.Tokens.Last() as Nonterminal, program, statementList);

        if (relational.Tokens.Length == 3)
        {
            var _operator = (relational.Tokens[1].Symbol as IHaveValue<string>).Value switch
            {
                "<" => OperatorType.LessThan,
                ">" => OperatorType.GreaterThan,
                "<=" => OperatorType.LessThanOrEqual,
                ">=" => OperatorType.GreaterThanOrEqual,
                _ => throw new()
            };

            return new OperatorExpression(new List<IExpression>() { GetRelational(relational.Tokens[0] as Nonterminal, program, statementList), additive }, _operator, program.OperatorsManager.GetOperator);
        }
        else
        {
            return additive;
        }
    }

    private IExpression GetAdditive(Nonterminal additive, Program program, IStatementList statementList)
    {
        if (additive.SymbolName != "Additive")
        {
            throw new InvalidDataException(nameof(additive));
        }

        var multiplicative = GetMultiplicative(additive.Tokens.Last() as Nonterminal, program, statementList);

        if (additive.Tokens.Length == 3)
        {
            var _operator = (additive.Tokens[1].Symbol as IHaveValue<string>).Value switch
            {
                "+" => OperatorType.Addition,
                "-" => OperatorType.Subtraction,
                _ => throw new()
            };

            return new OperatorExpression(new List<IExpression>() { GetAdditive(additive.Tokens[0] as Nonterminal, program, statementList), multiplicative }, _operator, program.OperatorsManager.GetOperator);
        }
        else
        {
            return multiplicative;
        }
    }

    private IExpression GetMultiplicative(Nonterminal multiplicative, Program program, IStatementList statementList)
    {
        if (multiplicative.SymbolName != "Multiplicative")
        {
            throw new InvalidDataException(nameof(multiplicative));
        }

        var exponent = GetExponent(multiplicative.Tokens.Last() as Nonterminal, program, statementList);

        if (multiplicative.Tokens.Length == 3)
        {
            var _operator = (multiplicative.Tokens[1].Symbol as IHaveValue<string>).Value switch
            {
                "*" => OperatorType.Multiplication,
                "/" => OperatorType.Division,
                "%" => OperatorType.Mod,
                _ => throw new()
            };

            return new OperatorExpression(new List<IExpression>() { GetMultiplicative(multiplicative.Tokens[0] as Nonterminal, program, statementList), exponent }, _operator, program.OperatorsManager.GetOperator);
        }
        else
        {
            return exponent;
        }
    }

    private IExpression GetExponent(Nonterminal exponent, Program program, IStatementList statementList)
    {
        if (exponent.SymbolName != "Exponent")
        {
            throw new InvalidDataException(nameof(exponent));
        }

        var factor = GetFactor(exponent.Tokens.First() as Nonterminal, program, statementList);

        if (exponent.Tokens.Length == 3)
        {
            return new OperatorExpression(new List<IExpression>() { GetExponent(exponent.Tokens[2] as Nonterminal, program, statementList), factor }, OperatorType.Exponent, program.OperatorsManager.GetOperator);
        }
        else
        {
            return factor;
        }
    }

    private IExpression GetFactor(Nonterminal factor, Program program, IStatementList statementList)
    {
        if (factor.SymbolName != "Factor")
        {
            throw new InvalidDataException(nameof(factor));
        }

        if (factor.Tokens.Length == 3)
        {
            return GetExpression(factor.Tokens[1] as Nonterminal, program, statementList);
        }
        else
        {
            return (factor.Tokens[0].Symbol as IHaveValue<string>).Value switch
            {
                "Value" => new InstanceExpression(GetValue(factor.Tokens[0] as Nonterminal)),
                "id" => new VariableExpression((factor.Tokens[0] as Terminal).Value, statementList.GetValue),
                "Negation" => GetNegation(factor.Tokens[0] as Nonterminal, program, statementList),
                "Not" => GetNot(factor.Tokens[0] as Nonterminal, program, statementList),
                "Factorial" => GetFactorial(factor.Tokens[0] as Nonterminal, program, statementList),
                "Input" => GetInput(factor.Tokens[0] as Nonterminal, program, statementList),
                "Cast" => GetCast(factor.Tokens[0] as Nonterminal, program, statementList),
                _ => throw new()
            };
        }
    }

    private IInstance<IType> GetValue(Nonterminal value)
    {
        return (value.Tokens[0].Symbol as IHaveValue<string>).Value switch
        {
            "int" => IntType.Instance.GetInstance(long.Parse((value.Tokens[0] as Terminal).Value)),
            "float" => FloatType.Instance.GetInstance(double.Parse((value.Tokens[0] as Terminal).Value.TrimEnd('f'))),
            "bool" => BoolType.Instance.GetInstance(bool.Parse((value.Tokens[0] as Terminal).Value)),
            "string" => StringType.Instance.GetInstance((value.Tokens[0] as Terminal).Value[1..^1]),
            _ => throw new()
        };
    }

    private IExpression GetNegation(Nonterminal negation, Program program, IStatementList statementList)
    {
        if (negation.SymbolName != "Negation")
        {
            throw new InvalidDataException(nameof(negation));
        }

        return new OperatorExpression(new List<IExpression>() { GetFactor(negation.Tokens[1] as Nonterminal, program, statementList) }, OperatorType.Negation, program.OperatorsManager.GetOperator);
    }
    
    private IExpression GetNot(Nonterminal not, Program program, IStatementList statementList)
    {
        if (not.SymbolName != "Not")
        {
            throw new InvalidDataException(nameof(not));
        }

        return new OperatorExpression(new List<IExpression>() { GetFactor(not.Tokens[1] as Nonterminal, program, statementList) }, OperatorType.Not, program.OperatorsManager.GetOperator);
    }    

    private IExpression GetFactorial(Nonterminal factorial, Program program, IStatementList statementList)
    {
        if (factorial.SymbolName != "Factorial")
        {
            throw new InvalidDataException(nameof(factorial));
        }

        return new OperatorExpression(new List<IExpression>() { GetFactor(factorial.Tokens[0] as Nonterminal, program, statementList) }, OperatorType.Factorial, program.OperatorsManager.GetOperator);
    }

    private IExpression GetInput(Nonterminal input, Program program, IStatementList statementList)
    {
        if (input.SymbolName != "Input")
        {
            throw new InvalidDataException(nameof(input));
        }

        return new InputExpression(program.ConsoleIn, input.Tokens.Length == 3 ? null : GetRawNonterminalList(input.Tokens[2] as Nonterminal).Select(n => GetExpression(n, program, statementList)).ToList());
    }

    private IExpression GetCast(Nonterminal cast, Program program, IStatementList statementList)
    {
        if (cast.SymbolName != "Cast")
        {
            throw new InvalidDataException(nameof(cast));
        }

        return new CastExpression(program.TypeManager.GetTypeByAlias((cast.Tokens[1] as Terminal).Value), GetFactor(cast.Tokens.Last() as Nonterminal, program, statementList));
    }
}
