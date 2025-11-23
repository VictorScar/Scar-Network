using System;
using System.Collections;
using System.Collections.Generic;
using ScarNetwork.Scriptes.Core;
using UnityEngine;
using UnityEngine.Serialization;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private LocalServerSettings serverSettings;
    [SerializeField] private NetworkConnector networkConnector;
    [SerializeField] private NetworkObjectStorage netObjStorage;

    private IServer _server;
    
    public static NetworkManager I { get; private set; }
    public NetworkConnector NetworkConnector => networkConnector;
    public NetworkObjectStorage NetObjectStorage => netObjStorage;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        networkConnector.Init();
        netObjStorage.Init();
    }

    public void StartServer()
    {
        _server.StartServer(serverSettings);
    }

    public void JoinToGame()
    {
        
    }
}