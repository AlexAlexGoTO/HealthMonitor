using MediatR;
using MedixineMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedixineMonitor.Domain.Events
{
    public class ObservationCreatedEvent : INotification
    {
        public ObservationCreatedEvent(Observation observation)
        {
            Observation = observation;
        }

        public Observation Observation { get; }
    }
}
