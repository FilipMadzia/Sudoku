using Sudoku.Mobile.ViewModels;
using Sudoku.Mobile.Views;

namespace Sudoku.Mobile;

public partial class App : Application
{
	private readonly LandingPage _landingPage;
	public App(LandingPage landingPage)
	{
		InitializeComponent();
		_landingPage = landingPage;
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var window = new Window(new NavigationPage(_landingPage))
		{
			Title = "Sudoku"
		};

		return window;
	}
}
