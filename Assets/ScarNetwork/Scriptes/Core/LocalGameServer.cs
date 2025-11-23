using System.Collections.Generic;
using ScarNetwork.Scriptes.Network_Player;
using UnityEngine;
using WebSocketSharp.Server;

public class LocalGameServer : MonoBehaviour
{
    private LocalServerSettings _serverSettings;
    private WebSocketServer _wss;

    private Dictionary<string, NetworkClient> _players = new Dictionary<string, NetworkClient>();

    public static LocalGameServer I { get; private set; }

    public LocalGameServer(LocalServerSettings serverSettings)
    {
        _serverSettings = serverSettings;
    }
    
    public bool IsStarted { get; private set; }

    public void StartServer()
    {
        if (I == null)
        {
            I = this;
            _wss = new WebSocketServer(_serverSettings.URL);
            _wss.AddWebSocketService<PlayerHandler>(_serverSettings.PlayerHandlerPath);
            _wss.Start();
            IsStarted = true;
            Debug.Log($"On server started HOST: {_serverSettings.Host} PORT: {_serverSettings.Port}");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StopServer()
    {
        if (_wss != null) _wss.Stop();
        I = null;
        IsStarted = false;
    }

    private void OnDestroy()
    {
        StopServer();
    }

    public void OnClientConnected(NetworkClient player)
    {
        _players.Add(player.ClientID, player);
        Debug.Log($"Client ID: {player.ClientID} Connect To Server");
    }

    public void OnClientDisconnected(NetworkClient player)
    {
        _players.Remove(player.ClientID);
    }

    public void OnClientMessage(string clientID, string msg)
    {
        var data = $"{clientID}: {msg}";
        Debug.Log($"Clietn with id: {clientID} Send Message: {msg}");

        SendNetworkMessage(msg);
    }

    public void SendNetworkMessage(string msgData)
    {
        
        
        foreach (var p in _players.Values)
        {
            p.Socket.Send(msgData);
        }
    }

    private string GeneratePlayerID(string root)
    {
        if (root == null)
        {
            root = "player";
        }

        return root + Random.Range(0, 1000);
    }
}