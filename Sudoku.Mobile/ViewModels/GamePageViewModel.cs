using Sudoku.Core;

namespace Sudoku.Mobile.ViewModels;

public class GamePageViewModel
{
	public Grid BuildBoard()
	{
		var sudokuBoard = SudokuBoardGenerator.GenerateEasy();

		var board = new Grid
		{
			RowSpacing = 1,
			ColumnSpacing = 1,
			BackgroundColor = Colors.Black,
			Padding = 2
		};

		for(var i = 0; i < 9; i++)
		{
			board.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
			board.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
		}

		for(var row = 0; row < 9; row++)
		for(var col = 0; col < 9; col++)
		{
			var cellValue = sudokuBoard[row, col].Value == 0
				? ""
				: sudokuBoard[row, col].Value.ToString();

			var cell = new Border
			{
				Content = new Label
				{
					Text = cellValue,
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					FontSize = 20
				},
				BackgroundColor = Colors.White,
				Padding = 0,
				Stroke = Colors.Black,
				StrokeThickness = 0.5
			};

			var top = (row % 3 == 0) ? 2 : 0.5;
			var left = (col % 3 == 0) ? 2 : 0.5;
			var right = (col == 8) ? 2 : 0.5;
			var bottom = (row == 8) ? 2 : 0.5;

			cell.Margin = new Thickness(left, top, right, bottom);

			Grid.SetRow(cell, row);
			Grid.SetColumn(cell, col);
			board.Children.Add(cell);
		}

		return board;
	}
}
