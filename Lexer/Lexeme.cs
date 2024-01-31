namespace Lexer;
public record Lexeme
{
    public string Type { get; init; }
    public string Value { get; init; }

    public Lexeme(string type, string value)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new ArgumentException($"'{nameof(type)}' cannot be null or whitespace.", nameof(type));
        }

        Type = type;
        Value = value;
    }
}
