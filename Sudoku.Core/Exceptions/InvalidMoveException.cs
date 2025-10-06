namespace Sudoku.Core.Exceptions;

public class InvalidMoveException : SudokuException
{
	public InvalidMoveException(SudokuMove move) : base($"Invalid move: {move}") {	}
}