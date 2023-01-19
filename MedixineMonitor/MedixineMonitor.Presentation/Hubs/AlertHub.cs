using MedixineMonitor.Application.Common.Abstraction;
using Microsoft.AspNetCore.SignalR;

namespace MedixineMonitor.Presentation.Hubs;

public class AlertHub : BaseAlertHub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("channel", message);
    }
}
