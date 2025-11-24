using System.Collections.Generic;
using Newtonsoft.Json;
using ScarNetwork.Scripts.Core;
using ScarNetwork.Scripts.Message_Data;
using ScarNetwork.Scripts.Network_Object;
using ScarNetwork.Scripts.Server;
using UnityEngine;

public class NetworkConnector : MonoBehaviour
{
    /*[SerializeField] private ServerSettings serverSettings;
    [SerializeField] private NetworkClient client;
    [SerializeField] private LocalGameServer _server;

    private string _localPlayerID;

    public bool IsMaster
    {
        get { return _server.IsStarted; }
    }

    public bool IsConnected => client.IsConnected;

    public bool IsMine(string objectID)
    {
        return true;
    }

    public void Init()
    {
        client.onReceiveMsg += OnReceiveMessage;
    }

    private void OnDestroy()
    {
        if (_server != null)
        {
            _server.StopServer();
        }

        if (client != null)
        {
            client.onReceiveMsg -= OnReceiveMessage;
        }
    }

    public void HostLocalGame()
    {
        _server = new LocalGameServer();
        _server.StartServer(serverSettings);
        JoinToLocalGame();
    }

    public void JoinToLocalGame()
    {
        client.JoinToServer(serverSettings.URL + serverSettings.PlayerHandlerPath);
    }

    public void SendRPC(ObjectMessageData objectMsgData)
    {
        var rpcDataStr = JsonConvert.SerializeObject(objectMsgData);

        var messageData = new NetworkMessageData
        {
            MessgeType = NetworkMessageType.RPC,
            Data = rpcDataStr
        };

        var json = JsonConvert.SerializeObject(messageData);
        SendNetworkMessage(json);
    }

    public void SendNetworkMessage(string msg)
    {
        client.SendNetworkMessage(msg);
    }

    private void OnReceiveMessage(string msg)
    {
        var messageData = JsonConvert.DeserializeObject<NetworkMessageData>(msg);

        switch (messageData.MessgeType)
        {
            case NetworkMessageType.Message:
                break;
            case NetworkMessageType.RPC:
                ProcessRPC(messageData.Data);
                break;
            case NetworkMessageType.Connection:
                ProcessConnection(messageData.Data);
                break;
        }
    }

    private void ProcessConnection(string messageData)
    {
        var connectionData = JsonConvert.DeserializeObject<ConnectionMessageData>(messageData);
        _localPlayerID = connectionData.ClientID;
    }

    private void ProcessRPC(string messageData)
    {
        var objData = JsonConvert.DeserializeObject<ObjectMessageData>(messageData);
        var nObj = NetworkManager.I.NetObjectStorage.GetObjectByID(objData.ObjectID);

        if (nObj)
        {
            var rpcMethodData = JsonConvert.DeserializeObject<RPCMethodData>(objData.Data);
            nObj.ReceiveRPC(rpcMethodData);
        }
        else
        {
            Debug.LogError($"NetworkObj by id {objData.ObjectID} not found");
        }
    }

    public void Disconnect()
    {
        client.Disconnect();
    }*/
}