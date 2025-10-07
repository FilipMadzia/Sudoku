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
		
		ValidateBoard(board);
		
		_board = board;
	}

	private void ValidateBoard(SudokuCell[,] board)
	{
		var rowCount = board.GetLength(0);
		var colCount = board.GetLength(1);
		
		if (rowCount != 9 || colCount != 9)
			throw new InvalidBoardException("Sudoku board size must be 9x9");
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