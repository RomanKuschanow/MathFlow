using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFlow.LexemeAnalyzer;
public class LexemeRow : IEnumerable<Lexeme>
{
    private List<Lexeme> lexemes = new();

    public void Add(Lexeme lexeme)
    {
        if (!string.IsNullOrWhiteSpace(lexeme.Value))
            lexemes.Add(lexeme);
    }

    public IEnumerator<Lexeme> GetEnumerator() => lexemes.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
