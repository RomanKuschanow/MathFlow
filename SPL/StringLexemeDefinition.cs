using Lexer;
using Lexer.LexemeDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SPL;
internal class StringLexemeDefinition : ILexemeDefinition
{
    public string Type => "string";

    public bool IsIgnored => false;

    public Lexeme? TryGetLexeme(string text)
    {
        Match match = Regex.Match(text, @"^""(?:\\\\|\\""|\\n|\\r|[^""\\])*""");

        return match.Success ? new(Type, Parse(match.Value), match.Value.Length) : null;
    }

    private string Parse(string input)
    {
        StringBuilder output = new StringBuilder();
        for (int i = 1; i < input.Length - 1; i++)
        {
            if (input[i] == '\\' && i + 1 < input.Length)
            {
                switch (input[i + 1])
                {
                    case '\\': output.Append('\\'); i++; break;
                    case '\"': output.Append('\"'); i++; break;
                    case 'r': output.Append('\r'); i++; break;
                    case 'n': output.Append('\n'); i++; break;
                    default: throw new InvalidDataException(nameof(input));
                }
            }
            else
            {
                output.Append(input[i]);
            }
        }

        var result = '"' + output.ToString() + '"';
        return result;
    }
}
