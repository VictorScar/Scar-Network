using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;

public class ClientComms : MonoBehaviour
{
    [SerializeField] private string userName = "ABOBA";
    private WebSocket _ws;

    public event Action<string> onReceiveMsg;

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

    public void JoinToServer(string url)
    {
        _ws = new WebSocket(url);

        _ws.OnOpen += OnOpen;
        _ws.OnMessage += OnMessage;
        _ws.OnClose += OnClose;
        _ws.AddUserHeader("username", userName);

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
        onReceiveMsg?.Invoke(e.Data);
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

    private void OnDestroy()
    {
        if (_ws != null)
        {
            _ws.Close();
        }
    }
}