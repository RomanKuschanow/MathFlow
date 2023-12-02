using MathFlow.LexemeAnalyzer;
using System.Text.RegularExpressions;

namespace MathFlow.Tests.LexemeAnalyzerFixtures;
public class RegexLexemeDefinitionFixtures
{
    [Fact]
    public void GivenValidRegexAndText_WhenTryGetLexeme_ThenReturnTrue()
    {
        // Arrange
        var regex = new Regex("[a-z]+");
        var type = LexType.None;
        var sut = new RegexLexemeDefinition(regex, type);

        // Act
        var result = sut.TryGetLexeme("test", out Lexeme lexeme);

        // Assert

        result.Should().BeTrue();
        lexeme.Value.Should().Be("test");
        lexeme.Type.Should().Be(type);
    }

    [Fact]
    public void GivenInvalidRegex_WhenTryGetLexeme_ThenReturnFalse()
    {
        // Arrange
        var regex = new Regex("^[a-z]+");
        var type = LexType.None;
        var sut = new RegexLexemeDefinition(regex, type);

        // Act
        var result = sut.TryGetLexeme("1234", out _);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenValidRegexAndText_WhenTryGetLexeme_ThenLexemeIsCorrect()
    {
        // Arrange
        var regex = new Regex("[a-z]+");
        var type = LexType.None;
        var sut = new RegexLexemeDefinition(regex, type);

        // Act
        var result = sut.TryGetLexeme("hello", out Lexeme lexeme);

        // Assert
        lexeme.Value.Should().Be("hello");
        lexeme.Type.Should().Be(type);
        result.Should().BeTrue();
    }

    [Fact]
    public void GivenEmptyText_WhenTryGetLexeme_ThenReturnFalse()
    {
        // Arrange
        var regex = new Regex("[a-z]+");
        var type = LexType.None;
        var sut = new RegexLexemeDefinition(regex, type);

        // Act
        var result = sut.TryGetLexeme("", out Lexeme lexeme);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenNullText_WhenTryGetLexeme_ThenThrowArgumentNullException()
    {
        // Arrange
        var regex = new Regex("[a-z]+");
        var type = LexType.None;
        var sut = new RegexLexemeDefinition(regex, type);

        // Act & Assert
        Action act = () => sut.TryGetLexeme(null, out Lexeme lexeme);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

}
