using System;
using Newtonsoft.Json;
using ScarNetwork.Scripts.Message_Data;
using ScarNetwork.Scripts.Network_Object;
using UnityEngine;
using WebSocketSharp;

namespace ScarNetwork.Scripts.Core
{
    public class NetworkClient
    {
        private readonly NetworkObjectStorage _storage;
        private readonly PlayerNetwork _player;
        private WebSocket _ws;

        public event Action<ConnectionMessageData> onPlayerConnection;

        public bool IsConnected
        {
            get
            {
                if (_ws != null)
                {
                    return _ws.IsAlive;
                }
                else
                {
                    return false;
                }
            }
        }

        public NetworkClient(NetworkObjectStorage storage, PlayerNetwork player)
        {
            _storage = storage;
            _player = player;
        }

        public void JoinToServer(string url)
        {
            _ws = new WebSocket(url);

            _ws.OnOpen += OnOpen;
            _ws.OnMessage += OnMessage;
            _ws.OnClose += OnClose;
            _ws.AddUserHeader("username", _player.NickName);

            try
            {
                _ws.Connect();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            _ws.RemoveUserHeader("username");
        }

        public void Disconnect()
        {
            if (_ws != null && _ws.IsAlive)
            {
                _ws.Close();
            }
        }

        private void OnClose(object sender, CloseEventArgs e)
        {
            Debug.Log("Client On Close");
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            Debug.Log($"Client ID: {e.Data}");


            ProcessMessage(e.Data);
        }

        private void OnOpen(object sender, EventArgs e)
        {
            Debug.Log("Client OnOpen");
        }

        public void SendNetworkMessage(string message)
        {
            if (_ws != null && _ws.IsAlive)
            {
                _ws.Send(message);
            }
            else
            {
                Debug.LogError("WS is not Connected");
            }
        }

        private void ProcessMessage(string msg)
        {
            var messageData = JsonConvert.DeserializeObject<NetworkMessageData>(msg);

            switch (messageData.MessgeType)
            {
                case NetworkMessageType.Connection:
                    ProcessConnection(messageData.Data, true);
                    break;
                case NetworkMessageType.OtherConnection:
                    ProcessConnection(messageData.Data);
                    break;
                case NetworkMessageType.Message:
                    break;
                case NetworkMessageType.RPC:
                    break;
                case NetworkMessageType.Event:
                    break;
                case NetworkMessageType.SyncField:
                    break;
            }
        }

        private void ProcessConnection(string data, bool isSelf = false)
        {
            try
            {
                var connectionData = JsonConvert.DeserializeObject<ConnectionMessageData>(data);

                if (isSelf)
                {
                    _player.SetID(connectionData.ClientID);
                }

                onPlayerConnection?.Invoke(connectionData);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void ProcessRPC(string data)
        {
            try
            {
                var objMsgData = JsonConvert.DeserializeObject<ObjectMessageData>(data);
                var netObj = _storage.GetObjectByID(objMsgData.ObjectID);

                if (netObj != null)
                {
                    //netObj.ReceiveRPC();
                }
                else
                {
                    Debug.LogError($"Network Object by ID {objMsgData.ObjectID} not found");
                }

            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}