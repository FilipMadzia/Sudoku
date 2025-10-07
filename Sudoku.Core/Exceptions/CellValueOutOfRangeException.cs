namespace Sudoku.Core.Exceptions;

public class CellValueOutOfRangeException : SudokuException
{
	public CellValueOutOfRangeException(int? value) : base($"Cell value: {value} if out of range: [1; 9]") {	}
}