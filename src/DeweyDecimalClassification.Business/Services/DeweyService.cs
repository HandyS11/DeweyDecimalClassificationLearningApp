using DeweyDecimalClassification.Business.Interfaces;
using DeweyDecimalClassification.Business.Models;
using DeweyDecimalClassification.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace DeweyDecimalClassification.Business.Services;

public class DeweyService(DeweyDecimalClassificationDbContext context) : IDeweyService
{
    public async Task<IEnumerable<SimplifiedDewey>> GetAllAsync()
    {
        var deweyEntries = await context.DeweyEntries.AsNoTracking()
            .Where(x => x.ParentId == null)
            .OrderBy(x => x.Id)
            .Select(x => new SimplifiedDewey
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
        
        return deweyEntries;
    }

    public async Task<IEnumerable<SimplifiedDewey>> GetSomeAsync(int count)
    {
        var deweyEntries = await context.DeweyEntries.AsNoTracking()
            .Select(x => new SimplifiedDewey
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();

        return deweyEntries.OrderBy(x => Guid.NewGuid()).Take(count);
    }

    public async Task<Dewey?> GetByIdAsync(float id)
    {
        var deweyEntry = await context.DeweyEntries.AsNoTracking()
            .Include(d => d.Parent)
            .Include(d => d.Children)
            .Where(x => x.Id.Equals(id))
            .Select(x => new Dewey
            {
                Id = x.Id,
                Name = x.Name,
                Parent = x.Parent == null ? null : new Dewey
                {
                    Id = x.Parent.Id,
                    Name = x.Parent.Name
                },
                Children = x.Children.Select(c => new Dewey
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return deweyEntry;
    }
}