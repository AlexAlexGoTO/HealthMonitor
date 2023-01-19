using AutoMapper;
using MediatR;
using MedixineMonitor.Application.Common.Dto;
using MedixineMonitor.Application.Common.Interfaces;
using MedixineMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedixineMonitor.Application.Observations.Queries;
public class GetObservationsQuery : IRequest<IList<ObservationDto>>
{

}

public class GetObservationsQueryHandler : IRequestHandler<GetObservationsQuery, IList<ObservationDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheStore _cacheStore;
    private readonly IMapper _mapper;

    public GetObservationsQueryHandler(
        IApplicationDbContext context, 
        ICacheStore cacheStore,
        IMapper mapper)
    {
        _context = context;
        _cacheStore = cacheStore;
        _mapper = mapper;
    }

    public async Task<IList<ObservationDto>> Handle(GetObservationsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            //place for pagination
            var observations = await _context.Observations.ToListAsync();

            var dtos = _mapper.Map<List<ObservationDto>>(observations);

            return dtos;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
}
