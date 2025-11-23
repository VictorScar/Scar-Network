using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using ScarNetwork.Scriptes.Network_Object;
using ScarToolkit.Button;
using UnityEngine;
using UnityEngine.Serialization;

public class NetworkBehavior : MonoBehaviour
{
    [SerializeField] protected NetworkObject _netObj;
    [SerializeField] private Vector3 dirtction = new Vector3(1, 1, 1);
    [SerializeField] private float force = 5f;
    public bool IsMaster { get; }
    public bool IsMine { get; }

    private void Awake()
    {
        _netObj.onReceiveRpc += OnReceiveRPC;
    }

    private void OnDestroy()
    {
        _netObj.onReceiveRpc -= OnReceiveRPC;
    }

    private void Start()
    {
      Generate();
    }

 
    public void Shoot(Vector3 direction, float distance)
    {
        Debug.Log($"Shoot in {direction} at {distance} distance");
    }



    public T NetworkInstantiate<T>(string prefabName, Transform parent) where T : MonoBehaviour
    {
        var prefab = Resources.Load(prefabName) as T;
        return Instantiate(prefab, parent);
    }

    private void OnReceiveRPC(RPCMethodData methodData)
    {
        if (_rpcsReceiveWrappers.TryGetValue(methodData.MethodName, out var wrapp))
        {
            wrapp.Invoke(methodData.Parameters);
        }
        else
        {
            Debug.LogError($"Wrap name of {methodData.MethodName} not found");
        }
    }


    //Wrapper

    protected virtual void Generate()
    {
        _rpcsReceiveWrappers.Add("Shoot", ShootRPCReceive);
    }

    //Call
    public virtual void CallRPC(string methodName)
    {
    }

    public virtual void  CallRPC<T>(string methodName, T param1)
    {
        if (methodName == "Shoot")
        {
        }
    }

    public virtual void CallRPC<T, T1>(string methodName, T param, T1 param1)
    {
        if (methodName == "Shoot")
        {
            if (param is Vector3 directiom)
            {
                if (param1 is float speed)
                {
                    ShootRPCSend(directiom, speed);
                }
            }
        }
    }

    public virtual void CallRPC<T, T1, T2>(string methodName, T param, T1 param1, T2 param2)
    {
    }

    protected Dictionary<string, Action<string>> _rpcsReceiveWrappers = new Dictionary<string, Action<string>>();


    //Generated

    private void ShootRPCSend(Vector3 direction, float speed)
    {
        var parameters = new ShootRPCData
        {
            direction = direction,
            speed = speed
        };
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        var parametersJson = JsonConvert.SerializeObject(parameters, settings);
        _netObj.CalRPC("Shoot", parametersJson);
    }

    private void ShootRPCReceive(string parameters)
    {
        var deserializeParameters = JsonConvert.DeserializeObject<ShootRPCData>(parameters);
        Shoot(deserializeParameters.direction, deserializeParameters.speed);
    }

    [Serializable]
    public struct ShootRPCData
    {
        public Vector3 direction;
        public float speed;
    }
}