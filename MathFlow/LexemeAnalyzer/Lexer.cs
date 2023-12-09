using MathFlow.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathFlow.LexemeAnalyzer;
public class Lexer
{
    private readonly IEnumerable<ILexemeDefinition> _definitions;

    public Lexer(IEnumerable<ILexemeDefinition> definitions)
    {
        _definitions = definitions;
    }

    public List<Lexeme> Analyze(string text)
    {
        List<Lexeme> lexemes = new();

        string[] rows = Regex.Split(text, @"(?<=\n)").Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()).ToArray();

        for (int i = 0; i < rows.Length; i++)
        {
            int startIndex = 0;
            while (startIndex < rows[i].Length)
            {
                bool foundLex = false;

                foreach (var def in _definitions)
                {
                    if (def.TryGetLexeme(rows[i][startIndex..], out Lexeme lex))
                    {
                        if (!string.IsNullOrWhiteSpace(lex.Value))
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
                    throw new UnrecognizedCharacterException(i, startIndex);
                }
            }
        }

        return lexemes;
    }
}
