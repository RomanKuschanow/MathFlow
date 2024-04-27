namespace Lexer;
public record Lexeme
{
    public string Type { get; init; }
    public string Value { get; init; }
    public int Length { get; init; }

    public Lexeme(string type, string value, int? length = null)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new ArgumentException($"'{nameof(type)}' cannot be null or whitespace.", nameof(type));
        }

        Type = type;
        Value = value;
        Length = length ?? Value.Length;
    }
}
