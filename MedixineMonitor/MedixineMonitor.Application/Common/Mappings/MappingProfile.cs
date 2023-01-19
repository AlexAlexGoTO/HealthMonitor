using AutoMapper;
using MedixineMonitor.Application.Common.Dto;
using MedixineMonitor.Application.Observations.Commands;
using MedixineMonitor.Domain.Entities;

namespace MedixineMonitor.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Observation, ObservationDto>()
            .ReverseMap();

        CreateMap<CreateOrUpdateObservationCommand, Observation>();
    }
}

