using Sudoku.Core.Enums;
using Sudoku.Core.Exceptions;

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
		
		Validate();
	}
	
	private void Validate()
	{
		switch (MoveType)
		{
			case SudokuMoveType.Value:
				if (Value is > 9 or < 1)
					throw new InvalidMoveException(this);
				break;
			case SudokuMoveType.Note:
				if (Note is > 9 or < 1)
					throw new InvalidMoveException(this);
				break;
		}
	}

	public override string ToString() => $"{MoveType} ({Row}, {Col}, {Value ?? Note})";
}