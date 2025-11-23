using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ScarNetwork.Scriptes.Network_Object
{
    public class NetworkObject : MonoBehaviour
    {
        private NetworkConnector _networkConnector;
        private int _objectID;
        private string _ownerID;

        public event Action<RPCMethodData> onReceiveRpc;

        public bool IsConnected => _networkConnector.IsConnected;
        public bool IsMaster => _networkConnector.IsMaster;

        public bool IsMine => _networkConnector.IsMine(_ownerID);

        private void Start()
        {
            NetworkManager.I.NetObjectStorage.RegisterObject(this);
            _networkConnector = NetworkManager.I.NetworkConnector;
            Debug.Log("NO Start");
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

            NetworkManager.I.NetworkConnector.SendRPC(objectNetworkData);
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