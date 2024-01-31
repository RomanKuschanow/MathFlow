namespace Lexer.Exceptions;
public class UnrecognizedCharacterException : Exception
{
    public int Row { get; }
    public int Column { get; }

    public UnrecognizedCharacterException(int row, int column, string message) : base(message)
    {
        Row = row;
        Column = column;
    }

    public UnrecognizedCharacterException(int row, int column) : this(row, column, $"Unrecognized character at {row}, {column}") { }
}
