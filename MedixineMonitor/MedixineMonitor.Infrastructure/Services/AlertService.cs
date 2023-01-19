using MedixineMonitor.Application.Common.Interfaces;
using MedixineMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedixineMonitor.Infrastructure.Services
{
    public class AlertService : IAlertService
    {
        private readonly IApplicationDbContext _context;
        public AlertService(IApplicationDbContext context) {
            _context = context;
        }

        public async Task CreateAlert(SystemAlert systemAlert)
        {
            await _context.SystemAlerts.AddAsync(systemAlert);

            await _context.SaveChangesAsync(new CancellationToken());
        }

        public async Task RemoveAlert(Guid id)
        {
            var alert = await _context.SystemAlerts.FirstOrDefaultAsync(sa => sa.Id == id);

            if(alert != null)
            {
                _context.SystemAlerts.Remove(alert);

                await _context.SaveChangesAsync(new CancellationToken());
            }
        }

        public async Task<IEnumerable<SystemAlert>> GetAlerts()
        {
            return await _context.SystemAlerts.ToListAsync();
        }
    }
}
