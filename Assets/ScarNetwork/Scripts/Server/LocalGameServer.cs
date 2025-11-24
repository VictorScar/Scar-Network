using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp.Server;

namespace ScarNetwork.Scripts.Server
{
    public class LocalGameServer: IServer
    {
        private WebSocketServer _wss;
        private Dictionary<string, ServerNetworkClient> _players = new Dictionary<string, ServerNetworkClient>();

        public static LocalGameServer I { get; private set; }


        public bool IsStarted { get; private set; }

        public void StartServer(ServerSettings serverSettings)
        {
            if (I == null)
            {
                I = this;
                _wss = new WebSocketServer(serverSettings.URL);
                _wss.AddWebSocketService<ClientHandler>(serverSettings.PlayerHandlerPath);
                _wss.Start();
                IsStarted = true;
                Debug.Log($"On server started HOST: {serverSettings.Host} PORT: {serverSettings.Port}");
            }
        }

        public void StopServer()
        {
            if (_wss != null) _wss.Stop();
            I = null;
            IsStarted = false;
        }

        public PlayerNetwork[] GetPlayers()
        {
            throw new System.NotImplementedException();
        }

        public void OnClientConnected(ServerNetworkClient player)
        {
            _players.Add(player.ClientID, player);
            
            Debug.Log($"Client ID: {player.ClientID} Connect To Server");
        }

        public void OnClientDisconnected(ServerNetworkClient player)
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
}