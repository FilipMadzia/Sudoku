using System.Text;
using Sudoku.Core.Enums;
using Sudoku.Core.Exceptions;

namespace Sudoku.Core;

public class SudokuBoard
{
	public SudokuCell this[int row, int col]
	{
		get => _board[row, col];
		set => _board[row, col] = value;
	}

	public List<SudokuMove> Moves { get; set; } = [];
	
	private readonly SudokuCell[,] _board = new SudokuCell[9, 9];

	public SudokuBoard(SudokuCell[,]? board = null)
	{
		if (board == null) return;
		
		_board = board;
		
		ValidateBoard();
	}

	public void MakeMove(SudokuMove move)
	{
		ValidateMove(move);
		
		switch (move.MoveType)
		{
			case SudokuMoveType.Value:
				_board[move.Row - 1, move.Col - 1].Value = move.Value;
				break;
			case SudokuMoveType.Note:
			{
				if (move.Note is null)
					throw new Exception("MakeMove(): move type Note yet Note is null");

				var cell = _board[move.Row - 1, move.Col - 1];
				var noteIndex = move.Note.Value - 1;

				cell.Notes[noteIndex] = !cell.Notes[noteIndex];
				break;
			}
		}
		
		Moves.Add(move);
	}
	
	public void RevertMove()
	{
		var lastMove = Moves.Last();
		Moves.Remove(lastMove);

		switch (lastMove.MoveType)
		{
			case SudokuMoveType.Value:
				_board[lastMove.Row - 1, lastMove.Col - 1].Value = null;
				break;
			case SudokuMoveType.Note:
			{
				if (lastMove.Note is null)
					throw new Exception("MakeMove(): move type Note yet Note is null");

				var cell = _board[lastMove.Row - 1, lastMove.Col - 1];
				var noteIndex = lastMove.Note.Value - 1;

				cell.Notes[noteIndex] = !cell.Notes[noteIndex];
				break;
			}
		}
	}

	private void ValidateMove(SudokuMove move)
	{
		var cell = _board[move.Row - 1, move.Col - 1];

		if (cell.IsGiven)
			throw new InvalidMoveException(move, "Cannot modify a given cell.");

		switch (move.MoveType)
		{
			case SudokuMoveType.Value:
			{
				if (move.Value is null)
					throw new InvalidMoveException(move, "Move type Value yet Value is null.");

				int value = move.Value.Value;

				// Row check
				for (int c = 0; c < 9; c++)
				{
					if (c == move.Col - 1) continue;
					if (_board[move.Row - 1, c].Value == value)
						throw new InvalidMoveException(move, $"Value {value} already exists in this row.");
				}

				// Column check
				for (int r = 0; r < 9; r++)
				{
					if (r == move.Row - 1) continue;
					if (_board[r, move.Col - 1].Value == value)
						throw new InvalidMoveException(move, $"Value {value} already exists in this column.");
				}

				// 3x3 box check
				int startRow = (move.Row - 1) / 3 * 3;
				int startCol = (move.Col - 1) / 3 * 3;

				for (int r = 0; r < 3; r++)
				{
					for (int c = 0; c < 3; c++)
					{
						int checkRow = startRow + r;
						int checkCol = startCol + c;

						if (checkRow == move.Row - 1 && checkCol == move.Col - 1)
							continue;

						if (_board[checkRow, checkCol].Value == value)
							throw new InvalidMoveException(move, $"Value {value} already exists in this subgrid.");
					}
				}

				break;
			}
			case SudokuMoveType.Note:
			{
				if (move.Note is null)
					throw new InvalidMoveException(move, "Move type Note yet Note is null.");
				break;
			}
		}
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
			if (row % 3 == 0)
				sb.AppendLine("+-------+-------+-------+");

			for (var col = 0; col < 9; col++)
			{
				if (col % 3 == 0)
					sb.Append("| ");

				var cell = _board[row, col];
				var value = cell?.Value?.ToString() ?? "_";

				if (cell is { Value: not null, IsGiven: true })
					sb.Append($"\u001b[90m{value}\u001b[0m "); // gray
				else if (cell is { Value: not null, IsGiven: false })
					sb.Append($"{value} "); // default color
				else
					sb.Append("_ ");
			}

			sb.AppendLine("|");
		}

		sb.AppendLine("+-------+-------+-------+");
		return sb.ToString();
	}

}