using SyntaxAnalyzer.Generator;
using SyntaxAnalyzer.Rules;
using SyntaxAnalyzer.Rules.Symbols;
using SyntaxAnalyzer.Tokens;

namespace SyntaxAnalyzer.Tests;
public class ParserFixtures
{
    [Fact]
    public void Given_When_Then()
    {
        // Arrange
        Grammar grammar = new(new List<List<string>>()
        {
            new() { "S'", "S" },
            new() { "S", "S", "C" },
            new() { "S", "C" },
            new() { "C", "c" },
            new() { "C", "d" },
        });
        RuleAnalyzer generator = new();
        Stack<IToken> input = new(new List<IToken>()
            {
            new InputEnd(),
            new Terminal(grammar.Terminals.First(), "c"),
            new Terminal(grammar.Terminals.Last(), "d"),
            new Terminal(grammar.Terminals.First(), "c"),
        });
        Parser sut = new(generator.Analyze(grammar));

        // Act
        var result = sut.Parse(input);

        // Assert
        Map(result).Should().ContainInOrder(new List<string>() { "S", "S", "S", "C", "c", "C", "d", "C", "c" });

        IEnumerable<string?> Map(Nonterminal nonterminal)
        {
            List<string?> result = new() { nonterminal.Symbol.ToString() };

            result.AddRange(nonterminal.Tokens.SelectMany(t =>
            {
                if (t is Nonterminal)
                {
                    return Map((Nonterminal)t);
                }
                else
                {
                    return new List<string>() { ((Terminal)t).Value };
                }
            }));

            return result;
        }
    }
}
