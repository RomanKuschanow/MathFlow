using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathFlow.LexemeAnalyzer;
public class RegexLexemeDefinition : ILexemeDefinition
{
    public Regex Regex { get; }
    public LexType Type { get; }
    public bool IsIgnored { get; }

    public RegexLexemeDefinition(Regex regex, LexType type, bool isIgnored = false)
    {
        Regex = regex;
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
