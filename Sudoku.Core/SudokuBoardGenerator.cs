namespace Sudoku.Core;

public static class SudokuBoardGenerator
{
	private const int RowCount = 9;
	private const int ColumnCount = 9;
	
	public static SudokuBoard GenerateFilled()
	{
		var board = new SudokuBoard();

		FillBoard(board);
		return board;
	}

	
	private static bool FillBoard(SudokuBoard board)
	{
		for (var row = 0; row < RowCount; row++)
		{
			for (var col = 0; col < ColumnCount; col++)
			{
				if (board[row, col] is { Value: not null })
					continue;

				// Try numbers 1–9 in random order
				var numbers = Enumerable.Range(1, 9).OrderBy(_ => Random.Shared.Next()).ToList();

				foreach (var num in numbers)
				{
					if (IsSafe(board, row, col, num))
					{
						board[row, col] = new SudokuCell(num, isGiven: true);

						if (FillBoard(board))
							return true;

						// backtrack
						board[row, col] = new SudokuCell();
					}
				}

				return false; // dead end
			}
		}

		return true; // all cells filled
	}

	private static bool IsSafe(SudokuBoard board, int row, int col, int value)
	{
		// Row
		for (var c = 0; c < ColumnCount; c++)
			if (board[row, c]?.Value == value)
				return false;

		// Col
		for (var r = 0; r < RowCount; r++)
			if (board[r, col]?.Value == value)
				return false;

		// 3x3 Box
		var startRow = row / 3 * 3;
		var startCol = col / 3 * 3;

		for (var r = 0; r < 3; r++)
		for (var c = 0; c < 3; c++)
			if (board[startRow + r, startCol + c]?.Value == value)
				return false;

		return true;
	}
}