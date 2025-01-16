using api.Models;

namespace api.Services;

public interface IUnitsService
{
    public Task<IEnumerable<ViewUnitModel>> GetAllActiveUnitsAsync();

    public Task<ViewUnitModel> CreateUnitAsync(CreateUnitModel model);

    public Task<ViewUnitModel> UpdateUnitAsync(Guid id, CreateUnitModel model);

    public Task ArchiveUnitAsync(Guid id);
}