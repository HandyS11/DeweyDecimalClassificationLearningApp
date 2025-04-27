using System.Globalization;
using System.Diagnostics;

namespace DeweyDecimalClassificationLearningApp.Helpers;

[ContentProperty(nameof(ImageName))]
public class LocalizedImageExtension : IMarkupExtension<ImageSource>
{
    public string ImageName { get; set; }
    public string ImageExtension { get; set; } = "png";

    public ImageSource ProvideValue(IServiceProvider serviceProvider)
    {
        if (string.IsNullOrEmpty(ImageName)) return null!;
        
        var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        Debug.WriteLine($"Current culture: {culture}");

        // Try localized version first
        if (culture != "en") // Skip if already default language
        {
            var localizedImage = $"{ImageName}_{culture}.{ImageExtension}";
            Debug.WriteLine($"Attempting to load localized image: {localizedImage}");
            
            try
            {
                var source = ImageSource.FromFile(localizedImage);
                if (source != null)
                {
                    Debug.WriteLine($"Successfully loaded localized image: {localizedImage}");
                    return source;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading localized image: {ex.Message}");
            }
        }

        // Fallback to default
        var defaultImage = $"{ImageName}.{ImageExtension}";
        Debug.WriteLine($"Loading default image: {defaultImage}");
        return ImageSource.FromFile(defaultImage);
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) 
        => ProvideValue(serviceProvider);
}