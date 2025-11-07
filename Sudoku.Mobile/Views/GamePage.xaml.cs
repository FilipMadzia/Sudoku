using Sudoku.Mobile.ViewModels;

namespace Sudoku.Mobile.Views;

public partial class GamePage : ContentPage
{
	public GamePage(GamePageViewModel gamePageViewModel)
	{
		InitializeComponent();
		
		BindingContext = gamePageViewModel;
		SudokuBoard.Children.Add(gamePageViewModel.BuildBoard());
	}
}