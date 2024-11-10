using System.Text.Json;
using DeweyDecimalClassification.EfCore.Context;
using DeweyDecimalClassification.EfCore.Entities;

const string jsonFilePath = "ddc_summary.json";

using var context = new DeweyDecimalClassificationDbContext();
context.Database.EnsureCreated();

var jsonString = File.ReadAllText(jsonFilePath);
var deweyEntries = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

if (deweyEntries != null)
{
    foreach (var deweyEntry in
        from entry in deweyEntries
        where entry.Value != "[Unassigned]" && entry.Value != "(Optional number)"
        select new DeweyEntry
        {
            Id = int.Parse(entry.Key),
            Name = entry.Value,
            ParentId = GetParentId(entry.Key)
        })
    {
        context.DeweyEntries.Add(deweyEntry);
    }
    context.SaveChanges();
}

return 0;

int? GetParentId(string key)
{
    if (key.Length != 3 || key.EndsWith("00"))
    {
        return null;
    }

    for (var i = key.Length - 1; i >= 0; i--)
    {
        var parentKey = key[..i].PadRight(3, '0');
        if (parentKey != key && deweyEntries.ContainsKey(parentKey))
        {
            return int.Parse(parentKey);
        }
    }
    return null;
}