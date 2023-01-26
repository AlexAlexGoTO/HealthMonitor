using AutoMapper;
using MediatR;
using MedixineMonitor.Application.Common.Interfaces;
using MedixineMonitor.Domain.Entities;
using MedixineMonitor.Domain.Enums;
using MedixineMonitor.Domain.Events;

namespace MedixineMonitor.Application.Observations.Commands;

public class CreateOrUpdateObservationCommand : IRequest<int>
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public HealthMetrics Type { get; set; }
    public double Value { get; set; }
    public int PatientId { get; set; }
    public string Description { get; set; }

}

public class CreateOrUpdateObservationCommandHandler : IRequestHandler<CreateOrUpdateObservationCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;

    public CreateOrUpdateObservationCommandHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IPublisher publisher)
    {
        _context = context;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<int> Handle(CreateOrUpdateObservationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<Observation>(request);

            if (request.Id != null)
            {
                _context.Observations.Update(entity);
            }
            else
            {
                entity.Id = Guid.NewGuid();

                _context.Observations.Add(entity);
            }

            var resutl = await _context.SaveChangesAsync(cancellationToken);

            //publish domain event after commiting transaction
            await NotifyIfNeeded(entity, request.Id);
            

            //Add hub for sending data to ui

            return resutl;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public async Task NotifyIfNeeded(Observation observation, Guid? requestId)
    {
        var operation = requestId == null? ObservationOperation.Add : ObservationOperation.Update;

        await _publisher.Publish(new ObservationsUpdatedEvent(observation, operation));
    }
}
