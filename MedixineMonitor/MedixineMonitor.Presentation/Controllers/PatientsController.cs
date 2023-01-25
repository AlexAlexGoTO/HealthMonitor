using MediatR;
using MedixineMonitor.Application.Common.Dto;
using MedixineMonitor.Application.Observations.Queries;
using MedixineMonitor.Application.Patients.Queries;
using MedixineMonitor.Presentation.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MedixineMonitor.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientsController : ApiControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public PatientsController(
        IHttpClientFactory httpClientFactory, 
        IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
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
        var result = await _httpClient.GetAsync($"/{id}");

        var response = await result.Content.ReadFromJsonAsync<PatientDto>();

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> Put(PatientDto patient)
    {
        var result = await _httpClient.PutAsJsonAsync($"", patient);

        var response = await result.Content.ReadFromJsonAsync<int>();

        return Ok(response);
    }
}
