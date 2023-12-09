using MathFlow.LexemeAnalyzer;

namespace MathFlow.Tests.LexemeAnalyzerFixtures;
public class LexerFixture
{
    [Theory]
    [InlineData(
        """
        num x = 1;
        """, 1)]
    [InlineData(
        """
        num x = 1;
        x = 4 + 5;
        """, 2)]
    [InlineData(
        """
        num x = 1;

        x = 4 + 5;
        """, 2)]
    [InlineData(
        """
        num x = 1;
        num y = 5;
        x = 4 + y;
        """, 3)]
    [InlineData(
        """
        num x = 1;
        num y = 5;
        x = 4 + y;
        print(x);
        """, 4)]
    public void GivenCode_WhenAnalyze_ThenLexemeListContainsGivenCountOfRows(string code, int rows)
    {
        // Arrange
        var sut = new Lexer(Constants.LexemeDefinitions);

        // Act
        var result = sut.Analyze(code);

        // Assert
        result.Count().Should().Be(rows);
    }

    [Theory]
    [MemberData(nameof(CodeWithLexemes))]
    public void GivenCode_WhenAnalyze_ThenLexemeListContainsGivenLexemes(string code, List<Lexeme> lexemes)
    {
        // Arrange
        var sut = new Lexer(Constants.LexemeDefinitions);

        // Act
        var result = sut.Analyze(code);

        // Assert
        result.SelectMany(r => r).Should().BeEquivalentTo(lexemes).And.ContainInOrder(lexemes);
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
