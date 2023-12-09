using System.Collections;
using System.Collections.Immutable;
using System.Data;
using System.Net.Http.Headers;

namespace MathFlow.SyntaxAnalyzer;
public class Parser
{
    private readonly ImmutableList<IRule> _rules;
    private readonly ImmutableList<Dictionary<string, int>> _actions;
    private readonly ImmutableList<Dictionary<string, int>> _goto;

    public Parser(ImmutableList<IRule> rules, ImmutableList<Dictionary<string, int>> actions, ImmutableList<Dictionary<string, int>> @goto)
    {
        _rules = rules;
        _actions = actions;
        _goto = @goto;
    }

    public NonTerminal Parse(Stack<Terminal> input)
    {
        ParserStack stack = new();

        while (input.Count > 0)
        {
            int action = _actions[stack.GetState()][input.Peek().Name];

            if (action > 0)
            {
                stack.Shift(input.Pop(), action);
            }
            else if (action < 0)
            {
                var rule = _rules[-action];

                stack.Reduce(rule);
                stack.GoTo(_goto[stack.GetState()][rule.NonTerminal]);
            }
            else
            {
                break;
            }
        }

        return stack.Accept();
    }
}
