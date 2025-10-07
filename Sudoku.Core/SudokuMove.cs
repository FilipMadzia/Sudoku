using Sudoku.Core.Enums;

namespace Sudoku.Core;

public class SudokuMove
{
	public int Row { get; set; }
	public int Col { get; set; }
	public SudokuMoveType MoveType { get; set; }
	public int? Value { get; set; }
	public int? Note { get; set; }

	public override string ToString() => $"{MoveType} ({Row}, {Col}, {Value ?? Note})";
}