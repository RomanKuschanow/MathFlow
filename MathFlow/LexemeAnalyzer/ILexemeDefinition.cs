using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFlow.LexemeAnalyzer;
public interface ILexemeDefinition
{
    bool TryGetLexeme(string text, out Lexeme lexeme);
}
