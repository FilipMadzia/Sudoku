using Sudoku.Mobile.Services;
using Sudoku.Mobile.Views;
using System.Windows.Input;

namespace Sudoku.Mobile.ViewModels;

public class LandingPageViewModel
{
	public ICommand StartGameCommand { get; }

	private readonly INavigationService _navigation;

	public LandingPageViewModel(INavigationService navigation, GamePageViewModel gamePageViewModel)
	{
		_navigation = navigation;
		StartGameCommand = new Command(async () => await StartGame(gamePageViewModel));
	}

	private async Task StartGame(GamePageViewModel gamePageViewModel)
	{
		await _navigation.NavigateToAsync(new GamePage(gamePageViewModel));
	}
}
