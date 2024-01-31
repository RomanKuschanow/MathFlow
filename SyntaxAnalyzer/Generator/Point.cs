using SyntaxAnalyzer.Rules;
using SyntaxAnalyzer.Rules.Symbols;
using SyntaxAnalyzer.Tokens;
using System.Collections.Immutable;
using System.Globalization;

namespace SyntaxAnalyzer.Generator;
public class Point
{
    public IRule Rule { get; init; }
    public int Cursor { get; init; }
    public ImmutableArray<ISymbol> Next { get; init; }
    public ISymbol FollowingSymbol => Cursor < Rule.Tokens.Length ? Rule.Tokens[Cursor] : new RuleEndSymbol();

    public Point(IRule rule, int cursor, IEnumerable<ISymbol> next)
    {
        if (next is null || next.Count() == 0)
        {
            throw new ArgumentException(nameof(next));
        }

        Rule = rule;
        Cursor = cursor;
        Next = next.ToImmutableArray();
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        if (obj is not Point)
        {
            return false;
        }

        var point = (Point)obj;

        if (Rule != point.Rule)
        {
            return false;
        }

        if (Cursor != point.Cursor)
        {
            return false;
        }

        if (!Next.SequenceEqual(point.Next))
        {
            return false;
        }

        return true;
    }

    public override string ToString() => $"[{Rule.NonTerminal} -> {string.Join(" ", Rule.Tokens.Take(Cursor))}.{string.Join(" ", Rule.Tokens.Skip(Cursor))}, {string.Join("/", Next)}]";
}
