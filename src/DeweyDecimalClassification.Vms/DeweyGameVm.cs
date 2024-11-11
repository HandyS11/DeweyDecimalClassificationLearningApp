using System.Collections.ObjectModel;
using System.Diagnostics;
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

    public ReadOnlyObservableCollection<SimplifiedDewey> DeweyEntries { get; private set; }
    private readonly ObservableCollection<SimplifiedDewey> _deweyEntries = [];

    [ObservableProperty]
    private float deweyId;
    
    [ObservableProperty]
    private int streak;

    public DeweyGameVm(IDeweyService deweyService)
    {
        _deweyService = deweyService;
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
        var isCorrect = selectedEntry.Id == DeweyId;
        
        Streak = isCorrect ? Streak + 1 : 0;
        
        var correctAnswer = _deweyEntries.FirstOrDefault(e => e.Id == DeweyId)?.Name;
        var message = isCorrect ? $"You won!" : $"You lost! The correct answer was {correctAnswer}.";
        
        var toast = Toast.Make(message, ToastDuration.Long);
        await toast.Show();
        
        await LoadDeweyEntriesAsync();
    }
}