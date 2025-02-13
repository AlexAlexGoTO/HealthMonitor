﻿using MediatR;
using MedixineMonitor.Application.Common.Abstraction;
using MedixineMonitor.Application.Common.Interfaces;
using MedixineMonitor.Domain.Entities;
using MedixineMonitor.Domain.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;

namespace MedixineMonitor.Application.Observations.EventHandlers;

public class ObservationsUpdatedEventHandler : INotificationHandler<ObservationsUpdatedEvent>
{
    private readonly ILogger<ObservationsUpdatedEventHandler> _logger;
    private readonly ICacheStore _cacheStore;
    private readonly IHubContext<BaseDataHub> _hubContext;
    private readonly IAlertService _alertService;

    public ObservationsUpdatedEventHandler(
        ILogger<ObservationsUpdatedEventHandler> logger, 
        ICacheStore cacheStore,
        IHubContext<BaseDataHub> hubContext,
        IAlertService alertService)
    {
        _logger = logger;
        _cacheStore = cacheStore;
        _hubContext = hubContext;
        _alertService = alertService;
    }

    public async Task Handle(ObservationsUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        var observation = notification.Observation;

        if(notification.Operation == ObservationOperation.Add) 
        { 
            //the idea is to skip another database round trip and keep alerts in MemoryStore, it can be Redis or something similar
            //in current scenario we keep only last few observations in memory
            //also it should be based on some logic and criterias so it can be different for each Type of observation
            //Also criterias should be specific individially for each person +/or default value
            var cashedObservation = _cacheStore.Get<List<double>>($"{observation.PatientId}_{observation.Type}");
            if (cashedObservation == null || cashedObservation.Count == 0)
            {
                var toCache = new List<double>() { observation.Value };

                _cacheStore.Add(toCache, $"{observation.PatientId}_{observation.Type}", TimeSpan.FromDays(5));
            }
            else
            {
                if (cashedObservation.Count > 2)
                {
                    var avarage = cashedObservation.TakeLast(3).Average();

                    //hardcoded condition 
                    //allowed value is value + 5%
                    var allowedValue = avarage += avarage * 0.05;

                    if (observation.Value > allowedValue)
                    {
                        var alert = new SystemAlert
                        {
                            Id = Guid.NewGuid(),
                            ItemId = observation.Id,
                            PatientId = observation.PatientId,
                            Message = $"{observation.Type.ToString()}; Message: Allowed value exceeded."
                        };

                        await _hubContext.Clients.All.SendAsync("new-alert", alert);

                        await _alertService.CreateAlert(alert);
                    }

                    cashedObservation.RemoveAt(0);
                }

                cashedObservation.Add(observation.Value);
            }
        }

        await _hubContext.Clients.All.SendAsync("observations-update", observation);
    }
}

