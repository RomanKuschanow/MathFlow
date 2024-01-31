using System.Collections.Immutable;
using SyntaxAnalyzer.Generator;
using SyntaxAnalyzer.Generator.Actions;
using SyntaxAnalyzer.Rules;
using SyntaxAnalyzer.Tokens;

namespace SyntaxAnalyzer;
public class Parser
{
    public LRTable Table { get; init; }

    public Parser(LRTable table)
    {
        Table = table ?? throw new ArgumentNullException(nameof(table));
    }

    public Nonterminal Parse(Stack<IToken> input)
    {
        ParserStack stack = new(Table.InitState);

        while (input.Count > 0)
        {
            var s = input.Peek();

            IAction action = Table.Actions.Single(a => a.InitState == stack.GetState() && a.Symbol == input.Peek().Symbol);

            if (action.Type == ActionType.Shift)
            {
                stack.Shift(input.Pop(), action as ShiftAction);
            }
            else if (action.Type == ActionType.Reduce)
            {
                stack.Reduce(action as ReduceAction);
                stack.GoTo(Table.Actions.Where(a => a is GotoAction). Select(g => (GotoAction)g).Single(g => g.InitState == stack.GetState() && g.Symbol == stack.GetSymbol()).DestState);
            }
            else if (action.Type == ActionType.Accept)
            {
                break;
            }
            else
            {
                throw new Exception("Wrong action detected");
            }
        }

        return stack.Accept();
    }
}
