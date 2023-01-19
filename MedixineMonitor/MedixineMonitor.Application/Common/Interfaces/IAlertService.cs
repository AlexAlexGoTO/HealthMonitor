using MedixineMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedixineMonitor.Application.Common.Interfaces
{
    public interface IAlertService
    {
        public Task CreateAlert(SystemAlert systemAlert);

        public Task RemoveAlert(Guid id);

        public Task<IEnumerable<SystemAlert>> GetAlerts();
    }
}
