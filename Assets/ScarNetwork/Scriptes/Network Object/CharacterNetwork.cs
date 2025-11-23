using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using ScarToolkit.Button;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterNetwork : NetworkBehavior
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 moving;

    private Vector3 _syncPosition;

    [SerializeField] private Vector3 dir = new Vector3(1, 0, 0);

    public void Move(Vector3 direction, float speed)
    {
        moving = direction * speed;
    }

    public void SyncPosition(Vector3 newPosition)
    {
        _syncPosition = newPosition;
    }

    private void Start()
    {
        Generate();
    }

    private void Update()
    {
        if (_netObj.IsMaster)
        {
            transform.position += transform.right * Input.GetAxis("Horizontal")* speed * Time.deltaTime;
            CallRPC(nameof(SyncPosition), transform.position);
        }
        else
        {
            transform.position = _syncPosition;
        }

    }

    [Button]
    public void TestMove()
    {
        CallRPC(nameof(Move), dir, speed);
    }

    //Wrap

    protected override void Generate()
    {
        base.Generate();
        _rpcsReceiveWrappers.Add("Move", MoveRPCReceive);
        _rpcsReceiveWrappers.Add("SyncPosition", SyncPositionRPCReceive);
    }

    public override void CallRPC<T>(string methodName, T param)
    {
        if (methodName == "SyncPosition")
        {
            if (param is Vector3 newPos)
            {
                SyncPositionSend(newPos);
            }
        }
    }

    public override void CallRPC<T, T1>(string methodName, T param, T1 param1)
    {
        if (methodName == "Move")
        {
            if (param is Vector3 directiom)
            {
                if (param1 is float speed)
                {
                    MoveRPCSend(directiom, speed);
                }
            }
        }
    }

    private void SyncPositionSend(Vector3 position)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        var parametersJson = JsonConvert.SerializeObject(position, settings);
        _netObj.CalRPC("SyncPosition", parametersJson);
    }

    private void SyncPositionRPCReceive(string newPosStr)
    {
        var newPos = JsonConvert.DeserializeObject<Vector3>(newPosStr);
        SyncPosition(newPos);
    }

    private void MoveRPCSend(Vector3 direction, float speed)
    {
        var parameters = new MoveRPCData
        {
            direction = direction,
            speed = speed
        };
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        var parametersJson = JsonConvert.SerializeObject(parameters, settings);
        _netObj.CalRPC("Move", parametersJson);
    }

    private void MoveRPCReceive(string parameters)
    {
        var deserializeParameters = JsonConvert.DeserializeObject<MoveRPCData>(parameters);
        Move(deserializeParameters.direction, deserializeParameters.speed);
    }

    [Serializable]
    public struct MoveRPCData
    {
        public Vector3 direction;
        public float speed;
    }
}