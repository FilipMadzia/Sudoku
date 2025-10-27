using System.Text;
using Sudoku.Core.Exceptions;

namespace Sudoku.Core;

public class SudokuBoard
{
	public SudokuCell this[int row, int col]
	{
		get => _board[row, col];
		set => _board[row, col] = value;
	}
	
	private readonly SudokuCell[,] _board = new SudokuCell[9, 9];

	public SudokuBoard(SudokuCell[,]? board = null)
	{
		if (board == null) return;
		
		_board = board;
		
		ValidateBoard();
	}

	private void ValidateBoard()
	{
		// 1. Check board dimensions
		var rowCount = _board.GetLength(0);
		var colCount = _board.GetLength(1);

		if (rowCount != 9 || colCount != 9)
			throw new InvalidBoardException("Sudoku board size must be 9x9.");

		// 2. Validate cell contents
		for (var r = 0; r < 9; r++)
		{
			for (var c = 0; c < 9; c++)
			{
				if (_board[r, c] == null)
					throw new InvalidBoardException($"Cell at ({r},{c}) is null.");

				int? value = _board[r, c].Value;

				if (value is < 1 or > 9)
					throw new InvalidBoardException($"Invalid value {value} at ({r},{c}). Must be 1–9 or null.");
			}
		}

		// Helper local function to check duplicates
		static void CheckDuplicates(IEnumerable<int?> values, string context)
		{
			var nums = values.Where(v => v.HasValue).Select(v => v.Value);
			if (nums.GroupBy(v => v).Any(g => g.Count() > 1))
				throw new InvalidBoardException($"Duplicate value found in {context}.");
		}

		// 3. Check rows
		for (var r = 0; r < 9; r++)
			CheckDuplicates(Enumerable.Range(0, 9).Select(c => _board[r, c].Value), $"row {r + 1}");

		// 4. Check columns
		for (var c = 0; c < 9; c++)
			CheckDuplicates(Enumerable.Range(0, 9).Select(r => _board[r, c].Value), $"column {c + 1}");

		// 5. Check 3×3 subgrids
		for (var boxRow = 0; boxRow < 3; boxRow++)
		{
			for (var boxCol = 0; boxCol < 3; boxCol++)
			{
				var values = new List<int?>();
				for (var r = 0; r < 3; r++)
				for (var c = 0; c < 3; c++)
					values.Add(_board[boxRow * 3 + r, boxCol * 3 + c].Value);

				CheckDuplicates(values, $"subgrid ({boxRow + 1},{boxCol + 1})");
			}
		}
	}

	public override string ToString()
	{
		var sb = new StringBuilder();

		for (var row = 0; row < 9; row++)
		{
			sb.Append('[');
			
			for (var col = 0; col < 9; col++)
			{
				var cell = _board[row, col];
				var value = cell?.Value?.ToString() ?? "_";
				
				sb.Append(value);

				if (col < 8)
					sb.Append(' ');
			}
			
			sb.Append(']');
			
			if (row < 8)
				sb.AppendLine();
		}

		return sb.ToString();
	}
}