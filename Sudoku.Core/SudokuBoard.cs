using Sudoku.Core.Exceptions;

namespace Sudoku.Core;

public class SudokuBoard
{
	private readonly int[,] _board = new int[9, 9];

	public int this[int row, int column]
	{
		get => _board[row, column];
		set => _board[row, column] = value;
	}

	public SudokuBoard(int[,]? board = null)
	{
		if (board == null) return;
		
		ValidateBoard();
	}

	private void ValidateBoard()
	{
		var rowCount = _board.GetLength(0);
		var columnCount = _board.GetLength(1);

		if (rowCount != 9 || columnCount != 9)
			throw new InvalidBoardException("Sudoku board must be 9x9");
	}
}