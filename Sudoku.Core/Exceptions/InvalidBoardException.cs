namespace Sudoku.Core.Exceptions;

public class InvalidBoardException : SudokuException
{
	public InvalidBoardException(string message) : base(message) { }
}