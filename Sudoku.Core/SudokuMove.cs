using Sudoku.Core.Enums;

namespace Sudoku.Core;

public class SudokuMove
{
	public int Row { get; set; }
	public int Col { get; set; }
	public SudokuMoveType MoveType { get; set; }
	public int? Value { get; set; }
	public int? Note { get; set; }

	public SudokuMove(SudokuMoveType moveType, int valueOrNote)
	{
		Validate();
		
		MoveType = moveType;
		
		switch (moveType)
		{
			case SudokuMoveType.Value:
				Value = valueOrNote;
				break;
			case SudokuMoveType.Note:
				Note = valueOrNote;
				break;
		}
	}
	
	// TODO: constructor validation
	private void Validate()
	{
		
	}

	public override string ToString() => $"{MoveType} ({Row}, {Col}, {Value ?? Note})";
}