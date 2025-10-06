namespace Sudoku.Core;

public class SudokuMove
{
	public int Row { get; set; }
	public int Column { get; set; }
	public int Value  { get; set; }

	public override string ToString() => $"({Row}, {Column}, {Value})";
}