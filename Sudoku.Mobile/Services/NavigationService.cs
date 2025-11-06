namespace Sudoku.Mobile.Services;

internal class NavigationService : INavigationService
{
	public async Task NavigateToAsync(Page page)
	{
		await Application.Current.MainPage.Navigation.PushAsync(page);
	}
}
