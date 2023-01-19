using AutoMapper;
using MediatR;
using MedixineMonitor.Application.Common.Dto;
using MedixineMonitor.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedixineMonitor.Application.Observations.Queries;

public class GetObservationQuery : IRequest<ObservationDto>
{
    public GetObservationQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class GetObservationQueryHandler : IRequestHandler<GetObservationQuery, ObservationDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheStore _cacheStore;
    private readonly IMapper _mapper;

    public GetObservationQueryHandler(
        IApplicationDbContext context,
        ICacheStore cacheStore,
        IMapper mapper)
    {
        _context = context;
        _cacheStore = cacheStore;
        _mapper = mapper;
    }

    public async Task<ObservationDto> Handle(GetObservationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var observation = await _context
                .Observations
                .FirstOrDefaultAsync(o => o.Id == request.Id);

            var dto = _mapper.Map<ObservationDto>(observation);

            return dto;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
}
