using MediatR;
using MedixineMonitor.Application.Common.Abstraction;
using MedixineMonitor.Application.Common.Dto;
using MedixineMonitor.Application.Observations.Queries;
using MedixineMonitor.Application.Patients.Queries;
using MedixineMonitor.Domain.Entities;
using MedixineMonitor.Presentation.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace MedixineMonitor.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientsController : ApiControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IHubContext<BaseDataHub> _hubContext;

    public PatientsController(
        IHttpClientFactory httpClientFactory, 
        IConfiguration configuration,
        IHubContext<BaseDataHub> hubContext)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
        _hubContext = hubContext;

        _httpClient.BaseAddress = new Uri(_configuration["PatientsServiceUrl"]!);
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var result = await Mediator.Send(new GetPatientsQuery());

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var result = await _httpClient.GetAsync($"{id}");

        var response = await result.Content.ReadFromJsonAsync<PatientDto>();

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> Put(PatientDto patient)
    {
        var result = await _httpClient.PutAsJsonAsync($"", patient);

        var response = await result.Content.ReadFromJsonAsync<int>();

        patient.Id = response;

        await _hubContext.Clients.All.SendAsync("patients-update", patient);

        return Ok(response);
    }
}
