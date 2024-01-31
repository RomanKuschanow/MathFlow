using SyntaxAnalyzer.Generator.Actions;
using SyntaxAnalyzer.Rules;
using SyntaxAnalyzer.Rules.Symbols;
using SyntaxAnalyzer.Tokens;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.PortableExecutable;
using static System.Net.Mime.MediaTypeNames;

namespace SyntaxAnalyzer.Generator;
public class RuleAnalyzer
{
    public LRTable Analyze(Grammar grammar)
    {
        if (grammar.Rules.Where(r => r.Tokens.Intersect(grammar.Terminals).SequenceEqual(r.Tokens)).Count() < 1)
        {
            throw new Exception("The list of rules must contain at least one rule that not contains any non terminal character");
        }

        IRule header = GetHeaderRule(grammar);

        var follows = Follows(header, grammar);

        List<State> states = new();
        List<Goto> gotos = new();

        Point initKernel = new(header, 0, new List<ISymbol> { AcceptSymbol.Instance });
        State initState = new(new List<Point>() { initKernel }, GetClosure(initKernel, grammar));

        states.Add(initState);

        for (int i = 0; i < states.Count; i++)
        {
            var uniqueSymbols = states[i].Closure.Select(p => p.FollowingSymbol).Where(s => s is not RuleEndSymbol).Distinct();

            if (uniqueSymbols.Count() < 1)
            {
                continue;
            }

            foreach (ISymbol symbol in uniqueSymbols)
            {
                List<Point> kernels = states[i].Closure
                    .Where(p => p.FollowingSymbol == symbol)
                    .Select(p => new Point(p.Rule, p.Cursor + 1, p.Next))
                    .ToList();
                State toState;

                if (states.Select(s => s.Kernel.ToList()).SingleOrDefault(k => k.SequenceEqual(kernels)) is not null)
                {
                    toState = states.Single(s => s.Kernel.SequenceEqual(kernels));
                }
                else
                {
                    toState = new(kernels, kernels.SelectMany(k => GetClosure(k, grammar)).Distinct());
                    states.Add(toState);
                }

                gotos.Add(new(states[i], symbol, toState));
            }
        }

        IEnumerable<IAction> actions = gotos
            .SelectMany(g =>
            {
                List<IAction> result = new();

                foreach (Point kernel in g.ToState.Kernel)
                {
                    if (kernel.FollowingSymbol is RuleEndSymbol)
                    {
                        foreach (ISymbol symbol in kernel.Next)
                        {
                            if (symbol.Type == SymbolType.Accept && kernel.Rule == header)
                            {
                                result.Add(new AcceptAction(g.ToState));
                            }
                            else
                            {
                                result.Add(new ReduceAction(kernel.Rule, g.ToState, symbol));
                            }
                        }
                    }

                    if (g.Symbol is NonterminalSymbol)
                    {
                        result.Add(new GotoAction(g.ToState, g.InitState, g.Symbol));
                    }
                    else
                    {
                        result.Add(new ShiftAction(g.ToState, g.InitState, g.Symbol));
                    }

                }

                return result;
            })
            .Distinct()
            .ToList();

        var conflicts = actions
            .GroupBy(a => new { a.InitState, a.Symbol })
            .Where(g => g.Count() > 1)
            .Select(g => new Conflict(g.First(), g.Last()))
            .ToList();

        actions = actions.Except(conflicts.Select(c => c.Second));

        return new LRTable(actions, initState, conflicts);
    }

    public IRule GetHeaderRule(Grammar grammar)
    {
        List<KeyValuePair<IRule, IRule>> graph = new();

        foreach (IRule rule in grammar.Rules)
        {
            var next = grammar.Rules.Where(r => r.Tokens.Contains(rule.NonTerminal) && r.NonTerminal != rule.NonTerminal).ToList();

            if (next.Count < 1)
            {
                next.Add(null!);
            }

            next.ForEach(n => graph.Add(new(rule, n)));
        }

        var headers = graph.Distinct().Where(n => n.Value is null);

        if (headers.Count() != 1)
        {
            throw new Exception("The list of rules must contain exactly one main rule");
        }

        return headers.First().Key;
    }

    public List<TerminalSymbol> First(ISymbol symbol, Grammar grammar)
    {
        if (symbol.Type == SymbolType.Terminal)
        {
            return new() { (TerminalSymbol)symbol };
        }

        var result = grammar.Rules
            .Where(r => r.NonTerminal == (NonterminalSymbol)symbol && r.Tokens.First() != symbol)
            .SelectMany(r => First(r.Tokens.First(), grammar))
            .ToList();

        return result;
    }

    public ImmutableDictionary<NonterminalSymbol, ImmutableArray<ISymbol>> Follows(IRule header, Grammar grammar)
    {
        Dictionary<NonterminalSymbol, List<ISymbol>> result = grammar.Nonterminals.ToDictionary(n => n, n => new List<ISymbol>());

        result[header.NonTerminal].Add(AcceptSymbol.Instance);

        foreach (var rule in grammar.Rules)
        {
            List<ISymbol> follow = new();

            for (int i = 0; i < rule.Tokens.Length; i++)
            {
                if (rule.Tokens[i] is NonterminalSymbol)
                {
                    if (i + 1 < rule.Tokens.Length)
                    {
                        result[(NonterminalSymbol)rule.Tokens[i]].AddRange(First(rule.Tokens[i + 1], grammar).Cast<ISymbol>());
                    }
                    else
                    {
                        result[(NonterminalSymbol)rule.Tokens[i]].AddRange(result[rule.NonTerminal]);
                    }
                }
            }
        }

        return result.ToImmutableDictionary(f => f.Key, f => f.Value.Distinct().ToImmutableArray());
    }

    public IEnumerable<ISymbol> Follow(NonterminalSymbol nonterminal, IEnumerable<Point> closure, Grammar grammar)
    {
        var result = closure
            .Where(p => p.Next.Count() > 0)
            .Where(p => p.FollowingSymbol is NonterminalSymbol && (NonterminalSymbol)p.FollowingSymbol == nonterminal)
            .Where(p =>
            {
                var a = new Point(p.Rule, p.Cursor + 1, p.Next);
                return a.FollowingSymbol is RuleEndSymbol || a.FollowingSymbol is VoidSymbol;
            })
            .SelectMany(p => p.Next);

        result = result.Concat(
            closure
            .Where(p => p.FollowingSymbol is NonterminalSymbol && (NonterminalSymbol)p.FollowingSymbol == nonterminal)
            .Where(p =>
            {
                var a = new Point(p.Rule, p.Cursor + 1, p.Next);
                return a.FollowingSymbol is TerminalSymbol || a.FollowingSymbol is NonterminalSymbol;
            })
            .SelectMany(p => First((new Point(p.Rule, p.Cursor + 1, p.Next)).FollowingSymbol, grammar)).Cast<ISymbol>()
            );

        return result.Distinct();
    }

    public IEnumerable<Point> GetClosure(Point kernel, Grammar grammar)
    {
        List<Point> result = new() { kernel };

        if (kernel.FollowingSymbol is not NonterminalSymbol)
        {
            return result;
        }

        Queue<Point> queue = new();
        queue.Enqueue(kernel);

        while (queue.Count > 0)
        {
            Point point = queue.Dequeue();

            if (point.FollowingSymbol is not NonterminalSymbol)
            {
                continue;
            }

            var points = grammar.Rules
                .Where(r => r.NonTerminal == (NonterminalSymbol)point.FollowingSymbol)
                .Select(r => new Point(r, 0, new List<ISymbol>() { new RuleEndSymbol() })).ToList();

            var next = Follow((NonterminalSymbol)point.FollowingSymbol, result.Concat(points), grammar);

            points = points.Select(p => new Point(p.Rule, 0, next)).ToList();

            points.ToList().ForEach(p =>
            {
                if (!result.Contains(p))
                {
                    queue.Enqueue(p);
                    result.Add(p);
                }
            });
        }

        return result.Distinct();
    }
}
