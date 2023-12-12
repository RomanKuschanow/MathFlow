#nullable disable
using MathFlow.LexemeAnalyzer;
using MathFlow.SyntaxAnalyzer;

namespace MathFlow.Tests.SyntaxAnalyzerFixtures;
public class ParserFixtures
{
    [Theory]
    [MemberData(nameof(CodeWithSyntaxTree))]
    public void GivenCode_WhenParse_ThenSyntaxTreeCorrect(string code, NonTerminal tree)
    {
        // Arrange
        var sut = new Parser(Constants.Rules, Constants.Actions, Constants.Goto);
        var converter = new LexemesToTokensConverter();
        var lexer = new Lexer(Constants.LexemeDefinitions);
        var lexemes = lexer.Analyze(code);

        // Act
        var result = sut.Parse(converter.Convert(lexemes));

        // Assert

        bool AreTreesEqual(NonTerminal tree1, NonTerminal tree2)
        {
            if (tree1 == null && tree2 == null)
                return true;
            if (tree1 == null || tree2 == null)
                return false;
            if (tree1.GetType() != tree2.GetType() || !tree1.Name.Equals(tree2.Name))
                return false;

            if (tree1.Tokens.Count != tree2.Tokens.Count)
                return false;
            for (int i = 0; i < tree1.Tokens.Where(t => t is NonTerminal).Count(); i++)
            {
                if (!AreTreesEqual(tree1.Tokens.Where(t => t is NonTerminal).ToList()[i] as NonTerminal, tree2.Tokens.Where(t => t is NonTerminal).ToList()[i] as NonTerminal))
                    return false;
            }

            return true;
        }
    }

    public static IEnumerable<object[]> CodeWithSyntaxTree()
    {
        yield return new object[]
        {
            """
            num x = 1;
            """,
            new NonTerminal(
                "StatementList",
                new IToken[]
                {
                    new NonTerminal(
                        "Statement",
                        new IToken[]
                        {
                            new NonTerminal(
                                "Declaration",
                                new IToken[]
                                {
                                    new Terminal("type", new Lexeme(LexType.Keyword, "num")),
                                    new Terminal("id", new Lexeme(LexType.Identifier, "x")),
                                    new Terminal("=", new Lexeme(LexType.Operator, "=")),
                                    new NonTerminal(
                                        "Expression",
                                        new IToken[]
                                        {
                                            new NonTerminal(
                                                "Term",
                                                new IToken[]
                                                {
                                                    new NonTerminal(
                                                        "Factor",
                                                        new IToken[]
                                                        {
                                                            new Terminal("number", new Lexeme(LexType.Number, "1"))
                                                        })
                                                })
                                        })
                                }),
                            new Terminal(";", new Lexeme(LexType.Separator, ";"))
                        })
                })
        };
        yield return new object[]
        {
            """
            num x = 1;
            x = 4 + 5;
            """,
            new NonTerminal(
                "StatementList",
                new IToken[]
                {
                    new NonTerminal(
                        "StatementList",
                        new IToken[]
                        {
                            new NonTerminal(
                                "Statement",
                                new IToken[]
                                {
                                    new NonTerminal(
                                        "Declaration",
                                        new IToken[]
                                        {
                                            new Terminal("type", new Lexeme(LexType.Keyword, "num")),
                                            new Terminal("id", new Lexeme(LexType.Identifier, "x")),
                                            new Terminal("=", new Lexeme(LexType.Operator, "=")),
                                            new NonTerminal(
                                                "Expression",
                                                new IToken[]
                                                {
                                                    new NonTerminal(
                                                        "Term",
                                                        new IToken[]
                                                        {
                                                            new NonTerminal(
                                                                "Factor",
                                                                new IToken[]
                                                                {
                                                                    new Terminal("number", new Lexeme(LexType.Number, "1"))
                                                                })
                                                        })
                                                })
                                        }),
                                    new Terminal(";", new Lexeme(LexType.Separator, ";"))
                                }),
                        }),
                    new NonTerminal(
                        "Statement",
                        new IToken[]
                        {
                            new NonTerminal(
                                "Assignment",
                                new IToken[]
                                {
                                    new Terminal("id", new Lexeme(LexType.Identifier, "x")),
                                    new Terminal("=", new Lexeme(LexType.Operator, "=")),
                                    new NonTerminal(
                                        "Expression",
                                        new IToken[]
                                        {
                                            new NonTerminal(
                                                "Expression",
                                                new IToken[]
                                                {
                                                    new NonTerminal(
                                                        "Term",
                                                        new IToken[]
                                                        {
                                                            new NonTerminal(
                                                                "Factor",
                                                                new IToken[]
                                                                {
                                                                    new Terminal("number", new Lexeme(LexType.Number, "4"))
                                                                })
                                                        })
                                                }),
                                            new Terminal("+", new Lexeme(LexType.Operator, "+")),
                                            new NonTerminal(
                                                "Term",
                                                new IToken[]
                                                {
                                                    new NonTerminal(
                                                        "Factor",
                                                        new IToken[]
                                                        {
                                                            new Terminal("number", new Lexeme(LexType.Number, "5"))
                                                        })
                                                })
                                        })
                                }),
                            new Terminal(";", new Lexeme(LexType.Separator, ";"))
                        }),
                })
        };
    }

}
