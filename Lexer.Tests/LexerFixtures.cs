using Lexer;
using Lexer.LexemeDefinitions;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace MathFlow.Tests.LexemeAnalyzerFixtures;
public class LexerFixtures
{
    public static ImmutableList<ILexemeDefinition> LexemeDefinitions => new List<ILexemeDefinition>()
    {
        new RegexLexemeDefinition(new Regex(@"""(?:\\\\|\\""|\\n|\\r|[^""\\])*"""), "String"),
        new RegexLexemeDefinition(new Regex(GetKeywordPattern(_keywords)), "Keyword"),
        new RegexLexemeDefinition(new Regex(@"^[\p{L}@_]+[\p{L}@_\d]*"), "Identifier"),
        new RegexLexemeDefinition(new Regex(@"^\b\d+(?:\.\d+)?(?:[Ee][+-]?\d+)?m?"), "Number"),
        new RegexLexemeDefinition(new Regex(@"^[-+*/=]"), "Operator"),
        new RegexLexemeDefinition(new Regex(@"^[();]"), "Separator"),
        new RegexLexemeDefinition(new Regex(@"^\s*"), "Space", true),
    }.ToImmutableList();

    private static string[] _keywords =
{
        "num",
        "string",
        "print",
    };

    private static string GetKeywordPattern(string[] keywords) => $"^{string.Join("|", keywords)}";

    [Theory]
    [MemberData(nameof(CodeWithLexemes))]
    public void GivenCode_WhenAnalyze_ThenLexemeListContainsGivenLexemes(string code, List<Lexeme> lexemes)
    {
        // Arrange
        var sut = new LexemeAnalyzer(LexemeDefinitions);

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
