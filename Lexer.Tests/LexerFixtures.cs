using Lexer;

namespace MathFlow.Tests.LexemeAnalyzerFixtures;
public class LexerFixtures
{

    [Theory]
    [MemberData(nameof(CodeWithLexemes))]
    public void GivenCode_WhenAnalyze_ThenLexemeListContainsGivenLexemes(string code, List<Lexeme> lexemes)
    {
        // Arrange
        var sut = new LexemeAnalyzer(Constants.LexemeDefinitions);

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
                new("Keyword", "num"),
                new("Identifier", "x"),
                new("Operator", "="),
                new("Number", "1"),
                new("Separator", ";")
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
                new("Keyword", "num"),
                new("Identifier", "x"),
                new("Operator", "="),
                new("Number", "1"),
                new("Separator", ";"),
                new("Identifier", "x"),
                new("Operator", "="),
                new("Number", "4"),
                new("Operator", "+"),
                new("Number", "5"),
                new("Separator", ";"),
            }
        };
    }
}
