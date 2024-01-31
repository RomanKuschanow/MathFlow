using Lexer.Exceptions;
using Lexer.LexemeDefinitions;

namespace Lexer;
public class LexemeAnalyzer
{
    private readonly IEnumerable<ILexemeDefinition> _definitions;

    public LexemeAnalyzer(IEnumerable<ILexemeDefinition> definitions)
    {
        _definitions = definitions;
    }

    public List<Lexeme> Analyze(string text)
    {
        List<Lexeme> lexemes = new();

        text = string.Join("", text.Where(ch => ch != '\r'));

        int startIndex = 0;
        while (startIndex < text.Length)
        {
            bool foundLex = false;

            foreach (var def in _definitions)
            {
                if (def.TryGetLexeme(text[startIndex..], out Lexeme lex))
                {
                    if (!def.IsIgnored)
                    {
                        lexemes.Add(lex);
                    }

                    foundLex = true;
                    startIndex += lex.Value.Length;
                    break;
                }
            }

            if (!foundLex)
            {
                int row = text[..startIndex].Where(ch => ch == '\n').Count() + 1;
                int column = startIndex - text[..startIndex].LastIndexOf("\n") + 1;

                throw new UnrecognizedCharacterException(row, column);
            }
        }

        return lexemes;
    }
}
