using Microsoft.AspNetCore.SignalR;

namespace queue_management.Hubs;

/// <summary>
/// QueueHub — Hub de SignalR para actualización en tiempo real.
///
/// ¿Qué es un Hub?
/// Es el punto central de comunicación entre el servidor y los clientes.
/// Piénsalo como un "sala de chat" donde el servidor puede hablar
/// con todos los clientes conectados (broadcast) o con uno específico.
///
/// Grupos disponibles:
///   "advisors"    — pestañas del panel de asesor
///   "waitingRoom" — pantalla de sala de espera
///
/// Métodos que el SERVIDOR llama en los clientes:
///   QueueUpdated()   — indica que la cola cambió (todos deben recargar datos)
/// </summary>
public class QueueHub : Hub
{
    // Cuando un cliente se conecta, puede unirse a un grupo según su rol.
    // El cliente llama: connection.invoke("JoinGroup", "advisors")
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}