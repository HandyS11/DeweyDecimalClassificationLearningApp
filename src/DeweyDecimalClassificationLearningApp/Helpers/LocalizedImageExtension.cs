using System.Globalization;

namespace DeweyDecimalClassificationLearningApp.Helpers;

[ContentProperty(nameof(BaseName))]
public class LocalizedImageExtension : IMarkupExtension<ImageSource>
{
    public string BaseName { get; set; } = null!;
    public string Extension { get; set; } = "png";

    public ImageSource ProvideValue(IServiceProvider serviceProvider)
    {
        if (string.IsNullOrEmpty(BaseName)) return null!;

        var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        var localizedImageName = $"{BaseName}_{lang}.{Extension}";
        var fallbackImageName = $"{BaseName}.{Extension}";

        var a = ImageSource.FromFile("dcc.png");
        return a;
            
        if (FileExists(localizedImageName))
            return ImageSource.FromFile(localizedImageName);

        if (FileExists(fallbackImageName))
            return ImageSource.FromFile(fallbackImageName);

        return null!;
    }

    private static bool FileExists(string filename)
    {
        try
        {
            var streamTask = FileSystem.OpenAppPackageFileAsync(filename);
            streamTask.Wait();
            return streamTask.Result != null;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        => ProvideValue(serviceProvider);
}