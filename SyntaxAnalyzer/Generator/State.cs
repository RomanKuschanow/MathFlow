using System.Collections.Immutable;

namespace SyntaxAnalyzer.Generator;

public record State
{
    public ImmutableArray<Point> Kernel { get; init; }
    public ImmutableArray<Point> Closure { get; init; }

    public State(IEnumerable<Point> kernel, IEnumerable<Point> closure)
    {
        Kernel = (kernel ?? throw new ArgumentNullException(nameof(kernel))).ToImmutableArray();
        Closure = (closure ?? throw new ArgumentNullException(nameof(closure))).ToImmutableArray();
    }

    public override string ToString() => $"{{{string.Join("; ", Closure)}}}";
}
