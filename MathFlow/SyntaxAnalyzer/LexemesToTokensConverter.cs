using MathFlow.LexemeAnalyzer;

namespace MathFlow.SyntaxAnalyzer;
public class LexemesToTokensConverter
{
    public Stack<IToken> Convert(List<Lexeme> lexemes)
    {
        lexemes.Reverse();
        Stack<IToken> tokens = new();

        tokens.Push(new InputEnd());

        foreach (Lexeme lexeme in lexemes)
        {
            switch (lexeme.Type)
            {
                case LexType.Keyword:
                    switch (lexeme.Value)
                    {
                        case "num":
                            tokens.Push(new Terminal("type", lexeme));
                            break;
                        case "print":
                            tokens.Push(new Terminal("print", lexeme));
                            break;
                    }
                    break;
                case LexType.Identifier:
                    tokens.Push(new Terminal("id", lexeme));
                    break;
                case LexType.Operator:
                case LexType.Separator:
                    tokens.Push(new Terminal(lexeme.Value, lexeme));
                    break;
                case LexType.Number:
                    tokens.Push(new Terminal("number", lexeme));
                    break;
                case LexType.Space:
                    break;
                case LexType.None:
                    break;
            }
        }

        return tokens;
    }
}
