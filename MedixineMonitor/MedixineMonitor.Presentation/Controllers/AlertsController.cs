using MedixineMonitor.Application.Common.Abstraction;
using MedixineMonitor.Application.Common.Interfaces;
using MedixineMonitor.Application.Observations.Queries;
using MedixineMonitor.Domain.Entities;
using MedixineMonitor.Domain.Enums;
using MedixineMonitor.Presentation.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MedixineMonitor.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AlertsController : ApiControllerBase
{
    private readonly IAlertService _alertService;
    private readonly IHubContext<BaseDataHub> _hubContext;

    public AlertsController(
        IAlertService alertService, 
        IHubContext<BaseDataHub> hubContext)
    {
        _alertService = alertService;
        _hubContext = hubContext;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var result = await _alertService.GetAlerts();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _alertService.RemoveAlert(id);

        return Ok();
    }
}
