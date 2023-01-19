using MedixineMonitor.Application.Common.Dto;
using MedixineMonitor.Application.Observations.Commands;
using MedixineMonitor.Application.Observations.Queries;
using MedixineMonitor.Presentation.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace MedixineMonitor.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ObservationsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var result = await Mediator.Send(new GetObservationsQuery());

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var result = await Mediator.Send(new GetObservationQuery(id));

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> Put(ObservationDto observation)
    {
        await Mediator.Send(new CreateOrUpdateObservationCommand
        {
            Id = observation.Id,
            Name = observation.Name,
            Value = observation.Value,
            Type = observation.Type,
            PatientId = observation.PatientId,
            Description = observation.Description
        });

        return Ok();
    }
}
