namespace Lexer.LexemeDefinitions;
public interface ILexemeDefinition
{
    public string Type { get; }
    public bool IsIgnored { get; }
    bool TryGetLexeme(string text, out Lexeme lexeme);
}
