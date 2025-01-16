using api.Data;
using api.Entities;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class UnitsService : IUnitsService
{
    private readonly AppDbContext _dbContext;

    public UnitsService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ViewUnitModel>> GetAllActiveUnitsAsync()
    {
        return await _dbContext.Units
            .Where(unit => !unit.IsArchived)
            .Select(u => u.ToViewUnitModel())
            .ToListAsync();
    }

    public async Task<ViewUnitModel> CreateUnitAsync(CreateUnitModel model)
    {
        var possibleUnit = FindUnitByName(model.Name);

        if (possibleUnit != null)
        {
            if (possibleUnit.IsArchived)
            {
                possibleUnit.IsArchived = false;
                await _dbContext.SaveChangesAsync();

                return possibleUnit.ToViewUnitModel();
            }

            throw new InvalidOperationException("Unit with this name already exists");
        }

        var unit = new Unit
        {
            Name = model.Name
        };

        _dbContext.Units.Add(unit);
        await _dbContext.SaveChangesAsync();

        return unit.ToViewUnitModel();
    }

    public async Task<ViewUnitModel> UpdateUnitAsync(Guid id, CreateUnitModel model)
    {
        var unit = await _dbContext.Units.FindAsync(id);

        if (unit == null) throw new InvalidOperationException("Unit not found");
        if (unit.IsArchived) throw new InvalidOperationException("Unit is archived");

        var possibleUnit = FindUnitByName(model.Name);

        if (possibleUnit != null && possibleUnit.Id != id)
        {
            if (possibleUnit.IsArchived)
            {
                await MergeUnits(possibleUnit, unit);

                return possibleUnit.ToViewUnitModel();
            }

            throw new InvalidOperationException("Unit with this name already exists");
        }

        unit.Name = model.Name;
        await _dbContext.SaveChangesAsync();

        return unit.ToViewUnitModel();
    }

    public async Task ArchiveUnitAsync(Guid id)
    {
        var unit = await _dbContext.Units.FindAsync(id);

        if (unit == null) throw new InvalidOperationException("Unit not found");
        if (unit.IsArchived) throw new InvalidOperationException("Unit is already archived");

        unit.IsArchived = true;
        await _dbContext.SaveChangesAsync();
    }

    private async Task MergeUnits(Unit oldUnit, Unit newUnit)
    {
        // New unit is edited and old unit is removed
        newUnit.Name = oldUnit.Name;
        _dbContext.Units.Remove(oldUnit);

        // TODO: Merge other properties and bind new unit to other entities where old unit was bound

        await _dbContext.SaveChangesAsync();
    }

    private Unit? FindUnitByName(string name)
    {
        return _dbContext.Units.FirstOrDefault(u => u.Name == name);
    }
}