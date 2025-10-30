namespace Sudoku.Core.Exceptions;

public class InvalidMoveException : SudokuException
{
	public InvalidMoveException(SudokuMove move) : base($"Invalid move: {move}") {	}
	public InvalidMoveException(SudokuMove move, string message) : base($"Invalid move: {move}\n" +
	                                                                    $"Message: {message}") {	}
}