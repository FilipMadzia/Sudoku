using Sudoku.Mobile.Services;
using Sudoku.Mobile.Views;
using System.Windows.Input;

namespace Sudoku.Mobile.ViewModels;

public class LandingPageViewModel
{
	public ICommand StartGameCommand { get; }

	private readonly INavigationService _navigation;

	public LandingPageViewModel(INavigationService navigation)
	{
		_navigation = navigation;
		StartGameCommand = new Command(async () => await StartGame());
	}

	private async Task StartGame()
	{
		await _navigation.NavigateToAsync(new GamePage());
	}
}
