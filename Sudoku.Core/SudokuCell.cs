using Sudoku.Core.Exceptions;

namespace Sudoku.Core;

public class SudokuCell
{
	public int? Value
	{
		get => _value;
		set
		{
			var isInRange = value is >= 1 and <= 9;
			
			if (value == null || isInRange)
				_value = value;
			else
				throw new CellValueOutOfRangeException(value);
		}
	}
	public bool[] Notes { get; } = new bool[9];
	public bool IsGiven { get; set; }

	private int? _value;

	public SudokuCell(int? value = null, bool isGiven = false)
	{
		Value = value;
		IsGiven = isGiven;
	}

	public override string ToString() => $"Value: {_value}, Notes: {Notes}";
}