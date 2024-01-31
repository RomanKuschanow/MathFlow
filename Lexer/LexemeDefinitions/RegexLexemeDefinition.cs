using System.Text.RegularExpressions;

namespace Lexer.LexemeDefinitions;
public class RegexLexemeDefinition : ILexemeDefinition
{
    public Regex Regex { get; }
    public string Type { get; }
    public bool IsIgnored { get; }

    public RegexLexemeDefinition(Regex regex, string type, bool isIgnored = false)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new ArgumentException($"'{nameof(type)}' cannot be null or whitespace.", nameof(type));
        }

        Regex = regex ?? throw new ArgumentNullException(nameof(regex));
        Type = type;
        IsIgnored = isIgnored;
    }

    public bool TryGetLexeme(string text, out Lexeme lexeme)
    {
        Match match;
        match = Regex.Match(text);

        lexeme = new(Type, match.Value);
        return match.Success;
    }
}
