using Microsoft.Extensions.Logging;
using CALKU.Data;
using CALKU.ViewModels;
namespace CALKU;

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

#if DEBUG
		builder.Logging.AddDebug();
        builder.Services.AddSingleton<DatabaseContext>();
        builder.Services.AddSingleton<DbContextYear>();
        builder.Services.AddSingleton<DatabaseContextSem2>();
        builder.Services.AddSingleton<SubjectsViewModel>();
        builder.Services.AddSingleton<SubjectsViewModel2>();
        builder.Services.AddSingleton<YearViewModel>();

        builder.Services.AddSingleton<Home>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddTransient<Subjects>();
        builder.Services.AddTransient<Subjects2>();
        
        builder.Services.AddSingleton<StudyYearsPage>();
        builder.Services.AddSingleton<StudyYearsSubjects>();
        builder.Services.AddSingleton<ISubjectsViewModel, SubjectsViewModel>();//dependecy injection
        builder.Services.AddSingleton<ISubjectsViewModel, SubjectsViewModel2>();//dependecy injection
        builder.Services.AddSingleton<IYearViewModel, YearViewModel>();//dependecy injection

        builder.Services.AddTransient<CGPA>();
        
#endif

        return builder.Build();
	}
}
