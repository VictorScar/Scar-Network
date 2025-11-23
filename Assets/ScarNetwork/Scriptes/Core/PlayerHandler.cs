using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ScarNetwork.Scriptes.Network_Player;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using Random = System.Random;

public class PlayerHandler : WebSocketBehavior
{
    private bool _isAthorized;
    private NetworkClient _player;

    public event Action<string, string> onSendMessage;
  
    protected override void OnOpen()
    {
        base.OnOpen();
        var username = Context.Headers["username"].ToString();
        var userID = GenerateUserID(username);
        Context.WebSocket.Send(userID);

        _player = new NetworkClient(new NetworkClientData
        {
            ClientID = userID,
            NickName = username,
            Socket = Context.WebSocket
        });

        LocalGameServer.I.OnClientConnected(_player);
    }

    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);
        Debug.Log("DisconnectFromServer");
        LocalGameServer.I.OnClientDisconnected(_player);
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);
        //Debug.Log($"OnMessage: {e.Data}");
        LocalGameServer.I.OnClientMessage(_player.ClientID, e.Data);
    }

    protected override void OnError(ErrorEventArgs e)
    {
        base.OnError(e);
        Debug.Log("OnError: " + e.Message);
    }

    public void SendData(string data)
    {
        Send(data);
    }

    private string GenerateUserID(string username)
    {
        if (username.IsNullOrEmpty())
        {
            username = "defaultPlayer";
        }

        var randomNumber = new Random().Next(0, 1000);

        return username + randomNumber;
    }
}