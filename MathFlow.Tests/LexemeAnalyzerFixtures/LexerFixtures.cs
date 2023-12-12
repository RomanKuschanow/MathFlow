using MathFlow.LexemeAnalyzer;

namespace MathFlow.Tests.LexemeAnalyzerFixtures;
public class LexerFixtures
{

    [Theory]
    [MemberData(nameof(CodeWithLexemes))]
    public void GivenCode_WhenAnalyze_ThenLexemeListContainsGivenLexemes(string code, List<Lexeme> lexemes)
    {
        // Arrange
        var sut = new Lexer(Constants.LexemeDefinitions);

        // Act
        var result = sut.Analyze(code);

        // Assert
        result.Should().BeEquivalentTo(lexemes).And.ContainInOrder(lexemes);
    }

    public static IEnumerable<object[]> CodeWithLexemes()
    {
        yield return new object[]
        {
            """
            num x = 1;
            """,
            new List<Lexeme>()
            {
                new(LexType.Keyword, "num"),
                new(LexType.Identifier, "x"),
                new(LexType.Operator, "="),
                new(LexType.Number, "1"),
                new(LexType.Separator, ";")
            }
        };
        yield return new object[]
        {
            """
            num x = 1;
            x = 4 + 5;
            """,
            new List<Lexeme>()
            {
                new(LexType.Keyword, "num"),
                new(LexType.Identifier, "x"),
                new(LexType.Operator, "="),
                new(LexType.Number, "1"),
                new(LexType.Separator, ";"),
                new(LexType.Identifier, "x"),
                new(LexType.Operator, "="),
                new(LexType.Number, "4"),
                new(LexType.Operator, "+"),
                new(LexType.Number, "5"),
                new(LexType.Separator, ";"),
            }        
        };
    }
}
