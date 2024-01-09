namespace MathFlow.LexemeAnalyzer;
public interface ILexemeDefinition
{
    bool TryGetLexeme(string text, out Lexeme lexeme);
}
