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

    public List<LexemeRow> Analyze(string text)
    {
        List<LexemeRow> lexemes = new();

        string[] rows = Regex.Split(text, @"(?<=\n)").Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()).ToArray();

        for (int i = 0; i < rows.Length; i++)
        {
            string[] subRows = Regex.Split(rows[i], @"(?<=;)").Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()).ToArray();

            for (int j = 0; j < subRows.Length; j++)
            {
                LexemeRow lexRow = new();

                int startIndex = 0;
                while (startIndex < subRows[j].Length)
                {
                    bool foundLex = false;

                    foreach (var def in _definitions)
                    {
                        if (def.TryGetLexeme(subRows[j][startIndex..], out Lexeme lex))
                        {
                            lexRow.Add(lex);
                            foundLex = true;
                            startIndex += lex.Value.Length;
                            break;
                        }
                    }

                    if (!foundLex)
                    {
                        throw new UnrecognizedCharacterException(i, subRows[..j].Select(sr => sr.Length).Sum() + startIndex);
                    }
                }

                lexemes.Add(lexRow);
            }
        }

        return lexemes;
    }
}
