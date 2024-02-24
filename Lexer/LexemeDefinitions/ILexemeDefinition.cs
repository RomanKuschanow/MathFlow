namespace Lexer.LexemeDefinitions;
public interface ILexemeDefinition
{
    public string Type { get; }
    public bool IsIgnored { get; }
    Lexeme? TryGetLexeme(string text);
}
