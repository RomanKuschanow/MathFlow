using SyntaxAnalyzer.Generator;
using SyntaxAnalyzer.Rules;
using SyntaxAnalyzer.Rules.Symbols;
using MathFlow;
using SyntaxAnalyzer.Generator.Actions;

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
    public void GivenRulesWithoutSimpleRule_WhenGenerateParsingTable_ThenExceptionWasThrown()
    {
        // Arrange
        Grammar grammar = new(new List<List<string>>()
        {
            new() { "S'", "S" },
            new() { "S", "S", "C" },
            new() { "S", "C" },
            new() { "C", "C", "c" },
            new() { "C", "C", "d" },
        });
        RuleAnalyzer sut = new();

        // Act
        var table = sut.Invoking(s =>s.Analyze(grammar))
        // Assert
            .Should().Throw<Exception>()
            .WithMessage("The list of rules must contain at least one rule that not contains any non terminal character");
    }

    [Fact]
    public void GivenRulesWithTwoHeaderRule_WhenGenerateParsingTable_ThenExceptionWasThrown()
    {
        // Arrange
        Grammar grammar = new(new List<List<string>>()
        {
            new() { "S'", "S" },
            new() { "D'", "S" },
            new() { "S", "S", "C" },
            new() { "S", "C" },
            new() { "C", "c" },
            new() { "C", "d" },
        });
        RuleAnalyzer sut = new();

        // Act
        var table = sut.Invoking(s =>s.Analyze(grammar))
        // Assert
            .Should().Throw<Exception>()
            .WithMessage("The list of rules must contain exactly one main rule");
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
    public void GivenNonconflictRulesWithALotOfRules_WhenGenerateParsingTable_ThenExpectedResult()
    {
        // Arrange
        RuleAnalyzer sut = new();

        // Act
        var table = sut.Analyze(Constants.Rules);

        // Assert
        table.Conflicts.Should().BeEmpty();
        table.Actions.Should().NotBeEmpty();
        table.Actions.Length.Should().Be(333);
        table.Actions.Where(a => a is GotoAction).Count().Should().Be(72);
        table.Actions.Where(a => a is ReduceAction).Count().Should().Be(152);
        table.Actions.Where(a => a is ShiftAction).Count().Should().Be(108);
        table.Actions.Where(a => a is AcceptAction).Count().Should().Be(1);
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
