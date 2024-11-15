using System.Globalization;
using CommunityToolkit.Maui;
using DeweyDecimalClassification.Business.Interfaces;
using DeweyDecimalClassification.Business.Services;
using DeweyDecimalClassification.EfCore.Context;
using DeweyDecimalClassification.Vms;
using DeweyDecimalClassificationLearningApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DeweyDecimalClassificationLearningApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "DeweyDecimalClassification.db");
        if (!File.Exists(dbPath))
        {
            using var stream = FileSystem.OpenAppPackageFileAsync("DeweyDecimalClassification.db").Result;
            using var newStream = File.Create(dbPath);
            stream.CopyTo(newStream);
        }

        builder.Services
            .AddDbContext<DeweyDecimalClassificationDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"))
            .AddSingleton<ILocalizationService, LocalizationService>()
            .AddSingleton<IDeweyService, DeweyService>()
            .AddSingleton<DeweyGameVm>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        /*var culture = new CultureInfo("fr-FR");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;*/
        
        return builder.Build();
    }
}
