namespace Sudoku.Core.Exceptions;

public class SudokuException : Exception
{
	public SudokuException() { }
	public SudokuException(string message) : base(message) { }
	public SudokuException(string message, Exception inner) : base(message, inner) { }
}