using Sudoku.Core;

namespace Sudoku.Mobile.ViewModels;

public class GamePageViewModel
{
	public Grid BuildBoard()
	{
		var sudokuBoard = SudokuBoardGenerator.GenerateFilled();

		var board = new Grid
		{
			RowSpacing = 1,
			ColumnSpacing = 1,
			BackgroundColor = Colors.Black,
			Padding = 2
		};

		for(int i = 0; i < 9; i++)
		{
			board.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
			board.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
		}

		for(int row = 0; row < 9; row++)
		{
			for(int col = 0; col < 9; col++)
			{
				var cell = new Border
				{
					Content = new Label
					{
						Text = sudokuBoard[row, col].Value!.ToString(),
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center
					},
					BackgroundColor = Colors.White,
					Padding = 0
				};

				Grid.SetRow(cell, row);
				Grid.SetColumn(cell, col);
				board.Children.Add(cell);
			}
		}

		return board;
	}
}
