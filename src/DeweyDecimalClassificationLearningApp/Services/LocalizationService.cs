using DeweyDecimalClassification.Business.Interfaces;
using DeweyDecimalClassificationLearningApp.Resources.Strings;

namespace DeweyDecimalClassificationLearningApp.Services;

public class LocalizationService : ILocalizationService
{
    public string GetString(string key, bool isDdc = false)
    {
        var nKey = isDdc ? $"Ddc_{key}" : key;
        var data = AppResources.ResourceManager.GetString(nKey);
        var str = data ?? key;
        return str;
    }
}