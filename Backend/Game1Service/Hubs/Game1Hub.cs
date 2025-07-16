using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CasinoGame.Facade.Models;
using CasinoGame.WalletService;
using Google.Protobuf.WellKnownTypes;
using LobbyService.Interfaces;
using Microsoft.AspNetCore.SignalR;


namespace GameService;

public interface IGameClient
{
    Task StartGame(GameTable table);
}

public class LobbyHub(IGameTableManager gameTableManager) : Hub<ILobbyClient>
{
    

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("we are on connected state");
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(userId))
        {
            UserConnections[userId] = Context.ConnectionId;
            Console.WriteLine($"User {userId} connected with ID {Context.ConnectionId}");
        }

        await base.OnConnectedAsync();
    }


    public async Task CancelGame(Guid tableId)
    {
        var refundResult = gameTableManager.CancelTable(tableId);
        await Clients.All.GameCanceled(tableId);
    }
}
