using Lexer.Exceptions;
using Lexer.LexemeDefinitions;
using System.Linq;

namespace Lexer;
public class LexemeAnalyzer
{
    private readonly IEnumerable<ILexemeDefinition> _definitions;

    public LexemeAnalyzer(IEnumerable<ILexemeDefinition> definitions)
    {
        _definitions = definitions;
    }

    //ToDo replace exceptions with warnings returned as a result
    public IEnumerable<Lexeme> Analyze(string text)
    {
        text = text.Replace("\r", ""); //string.Join("", text.Where(ch => ch != '\r'));

        int startIndex = 0;
        while (startIndex < text.Length)
        {
            var foundLexeme = _definitions
                .Select(definition => (lexeme: definition.TryGetLexeme(text[startIndex..]), definition))
                .FirstOrDefault(d => d.lexeme is not null);

            if (foundLexeme.lexeme is not null)
            {
                if (!foundLexeme.definition.IsIgnored)
                {
                    yield return foundLexeme.lexeme;
                }

                startIndex += foundLexeme.lexeme.Value.Length;
            }
            else
            {
                int row = text.Take(startIndex).Count(ch => ch == '\n') + 1;
                int column = startIndex - text[..startIndex].LastIndexOf("\n") + 1;

                throw new UnrecognizedCharacterException(row, column);
            }
        }
    }
}
