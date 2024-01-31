using SyntaxAnalyzer.Generator;
using SyntaxAnalyzer.Rules;
using SyntaxAnalyzer.Rules.Symbols;
using MathFlow;

namespace SyntaxAnalyzer.Tests;
public class GeneratorFixtures
{
    [Fact]
    public void GivenRules_WhenCreateGrammar_ThenRulesParsedCorrect()
    {
        // Arrange
        var grammar = new List<List<string>>()
        {
            new() { "S'", "S" },
            new() { "S", "C", "C" },
            new() { "C", "c", "C" },
            new() { "C", "d" },
        };
        Grammar sut;

        // Act
        sut = new(grammar);

        // Assert

        sut.Terminals.Select(t => t.Value).Should().Equal(new string[] { "c", "d" });
        sut.Nonterminals.Select(n => n.Value).Should().Equal(new string[] { "S'", "S", "C" });
        sut.Rules.Select(r => new List<string>() { r.NonTerminal.Value }.Concat(r.Tokens.Select(t => ((IHaveValue<string>)t).Value))).Select((r, i) => r.Should().Equal(grammar[i]));
    }

    [Fact]
    public void GivenNonconflictRulesWithOneHeaderRule_WhenGenerateParsingTable_ThenExpectedResult()
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
        RuleAnalyzer sut = new();

        // Act
        var table = sut.Analyze(grammar);

        // Assert
        table.Conflicts.Should().BeEmpty();
        table.Actions.Should().NotBeEmpty();
        table.Actions.Length.Should().Be(20);
    }

    [Fact]
    public void GivenConflictRulesWithOneHeaderRule_WhenGenerateParsingTable_ThenExpectedResult()
    {
        // Arrange
        Grammar grammar = new(new List<List<string>>()
        {
            new() { "S'", "S" },
            new() { "S", "S", "C" },
            new() { "S", "C" },
            new() { "C", "d", "C" },
            new() { "C", "d" },
        });
        RuleAnalyzer sut = new();

        // Act
        var table = sut.Analyze(grammar);

        // Assert
        table.Conflicts.Should().NotBeEmpty();
        table.Actions.Should().NotBeEmpty();
        table.Actions.Length.Should().Be(15);
    }
}
