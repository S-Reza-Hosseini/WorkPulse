using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPulse.RestApi.Auth;
using WorkPulse.Services.PlannerItems.Contracts;
using WorkPulse.Services.PlannerItems.Contracts.DTOs.Requests;
using WorkPulse.Services.PlannerItems.Exceptions;

namespace WorkPulse.RestApi.Controllers.PlannerItems;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlannerItemsController(
    IPlannerItemService service,
    IPlannerItemQuery query) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddPlannerItemDto dto)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        await service.Add(userId, dto);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        return Ok(await query.GetAll(userId));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        var item = await query.GetById(id, userId);
        if (item is null)
        {
            return NotFound($"Planner item with ID '{id}' not found.");
        }

        return Ok(item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdatePlannerItemDto dto)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            await service.Update(userId, id, dto);
            return Ok();
        }
        catch (PlannerItemNotFoundException)
        {
            return NotFound($"Planner item with ID '{id}' not found.");
        }
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeStatus([FromRoute] long id, [FromBody] ChangePlannerItemStatusDto dto)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            await service.ChangeStatus(userId, id, dto.Status);
            return Ok();
        }
        catch (PlannerItemNotFoundException)
        {
            return NotFound($"Planner item with ID '{id}' not found.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            await service.Delete(userId, id);
            return Ok();
        }
        catch (PlannerItemNotFoundException)
        {
            return NotFound($"Planner item with ID '{id}' not found.");
        }
    }
}
