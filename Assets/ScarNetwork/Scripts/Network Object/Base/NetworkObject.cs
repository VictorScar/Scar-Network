using System;
using Newtonsoft.Json;
using ScarNetwork.Scripts.Core;
using UnityEngine;

namespace ScarNetwork.Scripts.Network_Object
{
    public class NetworkObject : MonoBehaviour
    {
        private NetworkManager _networkManager;
        private int _objectID;
        private string _ownerID;

        public event Action<RPCMethodData> onReceiveRpc;

        public bool IsConnected => _networkManager.IsConnected;
        public bool IsMaster => _networkManager.IsMaster;

        public bool IsMine => _networkManager.IsMine(_ownerID);

        private void Start()
        {
            NetworkManager.I.NetObjectStorage.RegisterObject(this);
            _networkManager = NetworkManager.I;
            Debug.Log("network obj Start");
        }

        public void CalRPC(string methodName, string parameters)
        {
            var data = new RPCMethodData
            {
                MethodName = methodName,
                Parameters = parameters,
            };

            var rpcDataStr = JsonConvert.SerializeObject(data);

            var objectNetworkData = new ObjectMessageData
            {
                ObjectID = _objectID,
                Data = rpcDataStr
            };

           // NetworkManager.I.NetworkConnector.SendRPC(objectNetworkData);
        }

        public void ReceiveRPC(RPCMethodData methodData)
        {
            onReceiveRpc?.Invoke(methodData);
        }

        public void SetID(int id)
        {
            _objectID = id;
        }
    }

    public struct RPCMethodData
    {
        public string MethodName;
        public string Parameters;
    }
}