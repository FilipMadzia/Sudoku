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

	public static SudokuBoard GenerateEasy()
	{
		// Start with a full board
		var fullBoard = GenerateFilled();

		// Clone it so we can remove numbers
		var easyBoard = new SudokuBoard();
		for (var r = 0; r < RowCount; r++)
		for (var c = 0; c < ColumnCount; c++)
			easyBoard[r, c] = new SudokuCell(fullBoard[r, c].Value, isGiven: true);

		// Remove cells randomly but keep it solvable and relatively easy
		var allPositions = Enumerable.Range(0, RowCount * ColumnCount)
			.OrderBy(_ => Random.Shared.Next())
			.ToList();

		int removals = 40; // Easy difficulty (~40 empty cells)
		foreach (var pos in allPositions.Take(removals))
		{
			int row = pos / 9;
			int col = pos % 9;

			var backup = easyBoard[row, col];
			easyBoard[row, col] = new SudokuCell();

			// Optional: check for unique solvability — simplified check
			if (!HasUniqueSolution(easyBoard))
				easyBoard[row, col] = backup;
		}

		return easyBoard;
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
	
	private static bool HasUniqueSolution(SudokuBoard board)
	{
		int solutions = 0;
		bool Solve(int row, int col)
		{
			if (row == 9)
			{
				solutions++;
				return solutions > 1; // stop if more than one
			}

			int nextRow = col == 8 ? row + 1 : row;
			int nextCol = (col + 1) % 9;

			if (board[row, col].Value is { } v)
				return Solve(nextRow, nextCol);

			for (int num = 1; num <= 9; num++)
			{
				if (IsSafe(board, row, col, num))
				{
					board[row, col] = new SudokuCell(num);
					if (Solve(nextRow, nextCol))
					{
						board[row, col] = new SudokuCell();
						return true;
					}
					board[row, col] = new SudokuCell();
				}
			}

			return false;
		}

		Solve(0, 0);
		return solutions == 1;
	}
}