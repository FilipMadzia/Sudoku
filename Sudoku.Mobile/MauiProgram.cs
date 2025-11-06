using Microsoft.Extensions.Logging;
using Sudoku.Mobile.Services;
using Sudoku.Mobile.ViewModels;
using Sudoku.Mobile.Views;

namespace Sudoku.Mobile
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

			builder.Services.AddTransient<INavigationService, NavigationService>();

			builder.Services.AddTransient<LandingPage>();
			builder.Services.AddTransient<LandingPageViewModel>();

			builder.Services.AddTransient<GamePage>();
			builder.Services.AddTransient<GamePageViewModel>();

#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
