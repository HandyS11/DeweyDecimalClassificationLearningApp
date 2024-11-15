namespace DeweyDecimalClassification.Business.Interfaces;

public interface ILocalizationService
{
    string GetString(string key, bool isDdc = false);
}