using System.Collections.Generic;
using ScarNetwork.Scripts.Server;
using UnityEngine;


public class ClientManager : MonoBehaviour
{
    private List<ClientHandler> _players = new List<ClientHandler>();
    
    public static ClientManager I { get; private set; }

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddClient(ClientHandler client)
    {
        _players.Add(client);
    }
    
    public void RemoveClient(ClientHandler client)
    {
        _players.Remove(client);
    }
}
