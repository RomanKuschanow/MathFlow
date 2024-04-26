using SyntaxAnalyzer.Rules.Symbols;
using System.Collections.Immutable;

namespace SyntaxAnalyzer.Rules;
public class Grammar
{
    public ImmutableArray<IRule> Rules { get; init; }
    public ImmutableArray<NonterminalSymbol> Nonterminals { get; init; }
    public ImmutableArray<TerminalSymbol> Terminals { get; init; }

    public Grammar(IEnumerable<IEnumerable<string>> stringRules)
    {
        List<IRule> rules = new();
        Nonterminals = stringRules
            .Select(r => r.First())
            .Distinct()
            .Select(n => new NonterminalSymbol(n))
            .ToImmutableArray();
        Terminals = stringRules
            .Select(r => r.Skip(1))
            .SelectMany(r => r)
            .Distinct()
            .Except(Nonterminals.Select(n => n.Value))
            .Select(t => string.IsNullOrWhiteSpace(t) ? new VoidSymbol() : new TerminalSymbol(t)).ToImmutableArray();

        foreach (var stringRule in stringRules)
        {
            NonterminalSymbol nonterminal = Nonterminals.Single(n => n.Value == stringRule.First());
            var symbols = stringRule
                .Skip(1)
                .Select(s => Terminals.Cast<IHaveValue<string>>().Concat(Nonterminals).Single(_s => _s.Value == s))
                .Cast<ISymbol>()
                .ToArray();

            Rule rule = new(
                nonterminal,
                symbols);

            rules.Add(rule);
        }

        Rules = rules.ToImmutableArray();
    }
}
