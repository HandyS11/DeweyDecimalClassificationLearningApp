using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeweyDecimalClassification.Business.Interfaces;
using DeweyDecimalClassification.Business.Models;

namespace DeweyDecimalClassification.Vms;

public partial class DeweyGameVm : ObservableObject
{
    private readonly IDeweyService _deweyService;
    private readonly ILocalizationService _localizationService;

    public ReadOnlyObservableCollection<SimplifiedDewey> DeweyEntries { get; private set; }
    private readonly ObservableCollection<SimplifiedDewey> _deweyEntries = [];

    [ObservableProperty]
    private float _deweyId;

    [ObservableProperty]
    private int _streak;

    public DeweyGameVm(IDeweyService deweyService, ILocalizationService localizationService)
    {
        _deweyService = deweyService;
        _localizationService = localizationService;
        DeweyEntries = new ReadOnlyObservableCollection<SimplifiedDewey>(_deweyEntries);
    }

    [RelayCommand]
    public async Task SetupAsync()
    {
        Streak = 0;
        await LoadDeweyEntriesAsync();
    }

    public async Task LoadDeweyEntriesAsync()
    {
        var entries = await _deweyService.GetSomeAsync(4);
        var simplifiedDeweys = entries.ToList();

        _deweyEntries.Clear();

        foreach (var entry in simplifiedDeweys)
        {
            var idString = ParseFloat(entry.Id);
            var translatedName = _localizationService.GetString(idString, true);
            entry.Name = translatedName;
            Debug.WriteLine($"Id: {entry.Id}, Name: {entry.Name}");
            _deweyEntries.Add(entry);
        }

        var random = new Random();
        var i = random.Next(0, simplifiedDeweys.Count);
        var id = simplifiedDeweys.ToList()[i].Id;
        Debug.WriteLine($"Selected Id: {id}");
        DeweyId = id;
    }

    [RelayCommand]
    public async Task SelectDeweyEntryAsync(SimplifiedDewey selectedEntry)
    {
        var isCorrect = selectedEntry.Id.Equals(DeweyId);

        Streak = isCorrect ? Streak + 1 : 0;
        
        var correctAnswer = _deweyEntries.FirstOrDefault(e => e.Id.Equals(DeweyId))?.Name;
        var message = isCorrect
            ? _localizationService.GetString("VictoryMessage")
            : string.Format(_localizationService.GetString("DefeatMessage"), correctAnswer);

        var toast = Toast.Make(message, ToastDuration.Long);
        await toast.Show();

        await LoadDeweyEntriesAsync();
    }
    
    private static string ParseFloat(float value)
    {
        var idParts = value.ToString(CultureInfo.InvariantCulture).Split('.');
        var integerPart = int.Parse(idParts[0]).ToString("D3");
        var fractionalPart = idParts.Length > 1 ? "." + idParts[1] : string.Empty;
        return integerPart + fractionalPart;
    }
}