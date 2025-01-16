using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("units")]
public class UnitsController : ControllerBase
{
    private readonly IUnitsService _unitsService;

    public UnitsController(IUnitsService unitsService)
    {
        _unitsService = unitsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ViewUnitModel>>> Get()
    {
        var units = await _unitsService.GetAllActiveUnitsAsync();
        return Ok(units);
    }

    [HttpPost]
    public async Task<ActionResult<ViewUnitModel>> Post(
        CreateUnitModel model)
    {
        try
        {
            var unit = await _unitsService.CreateUnitAsync(model);
            return CreatedAtAction(nameof(Get), unit);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<ViewUnitModel>> Put(
        [FromRoute] Guid id,
        CreateUnitModel model)
    {
        try
        {
            var unit = await _unitsService.UpdateUnitAsync(id, model);
            return Ok(unit);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id)
    {
        try
        {
            await _unitsService.ArchiveUnitAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }
}