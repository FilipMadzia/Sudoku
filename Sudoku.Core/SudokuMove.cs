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

	public SudokuMove(int row, int col, int valueOrNote, SudokuMoveType moveType = SudokuMoveType.Value)
	{
		Row = row;
		Col = col;
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
		var rowOutOfRange = Row is < 1 or > 9;
		var colOutOfRange = Col is < 1 or > 9;

		if (rowOutOfRange || colOutOfRange)
			throw new InvalidMoveException(this, "Row or col out of range");
		
		switch (MoveType)
		{
			case SudokuMoveType.Value:
				if (Value is > 9 or < 1)
					throw new InvalidMoveException(this, "Value is out of range");
				break;
			case SudokuMoveType.Note:
				if (Note is > 9 or < 1)
					throw new InvalidMoveException(this, "Note is out of range");
				break;
		}
	}

	public override string ToString() => $"{MoveType} ({Row}, {Col}, {Value ?? Note})";
}