using Sudoku.Mobile.ViewModels;

namespace Sudoku.Mobile.Views;

public partial class LandingPage : ContentPage
{
	public LandingPage(LandingPageViewModel landingPageViewModel)
	{
		InitializeComponent();

		BindingContext = landingPageViewModel;
	}
}