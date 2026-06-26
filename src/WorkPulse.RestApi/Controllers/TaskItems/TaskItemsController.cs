using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPulse.RestApi.Auth;
using WorkPulse.Services.TaskItems.Contracts;
using WorkPulse.Services.TaskItems.Contracts.DTOs.Requests;
using WorkPulse.Services.TaskItems.Exceptions;

namespace WorkPulse.RestApi.Controllers.TaskItems;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskItemsController(
    ITaskItemService taskItemService,
    ITaskItemQuery taskItemQuery) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddTaskItemDto dto)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            await taskItemService.Add(userId, dto);
            return Ok();
        }
        catch (InsufficientTaskPermissionException)
        {
            return Forbid();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] long? teamId)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            return Ok(await taskItemQuery.GetAll(userId, teamId));
        }
        catch (InsufficientTaskPermissionException)
        {
            return Forbid();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            var taskItem = await taskItemQuery.GetById(userId, id);
            if (taskItem is null)
            {
                return NotFound($"Task with ID '{id}' not found.");
            }

            return Ok(taskItem);
        }
        catch (InsufficientTaskPermissionException)
        {
            return Forbid();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateTaskItemDto dto)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            await taskItemService.Update(userId, id, dto);
            return Ok();
        }
        catch (TaskItemNotFoundException)
        {
            return NotFound($"Task with ID '{id}' not found.");
        }
        catch (InsufficientTaskPermissionException)
        {
            return Forbid();
        }
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeStatus([FromRoute] long id, [FromBody] ChangeTaskItemStatusDto dto)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        try
        {
            await taskItemService.ChangeStatus(userId, id, dto.Status);
            return Ok();
        }
        catch (TaskItemNotFoundException)
        {
            return NotFound($"Task with ID '{id}' not found.");
        }
        catch (InsufficientTaskPermissionException)
        {
            return Forbid();
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
            await taskItemService.Delete(userId, id);
            return Ok();
        }
        catch (TaskItemNotFoundException)
        {
            return NotFound($"Task with ID '{id}' not found.");
        }
        catch (InsufficientTaskPermissionException)
        {
            return Forbid();
        }
    }
}
