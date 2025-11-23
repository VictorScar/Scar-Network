using System.Collections.Generic;
using ScarNetwork.Scriptes.Network_Player;
using UnityEngine;


public class ClientManager : MonoBehaviour
{
    private List<PlayerHandler> _players = new List<PlayerHandler>();
    
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

    public void AddClient(PlayerHandler client)
    {
        _players.Add(client);
    }
    
    public void RemoveClient(PlayerHandler client)
    {
        _players.Remove(client);
    }
}
