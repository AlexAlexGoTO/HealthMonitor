using MediatR;
using MedixineMonitor.Domain.Entities;

namespace MedixineMonitor.Domain.Events
{
    public enum ObservationOperation
    {
        Add,
        Update
    }

    public class ObservationsUpdatedEvent : INotification
    {
        public ObservationsUpdatedEvent(Observation observation, ObservationOperation operation)
        {
            Observation = observation;
            Operation = operation;
        }

        public Observation Observation { get; }
        public ObservationOperation Operation { get; }
    }
}
