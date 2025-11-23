using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Configs/LocalServer", fileName = "LocalServerSettings")]
public class LocalServerSettings : ScriptableObject
{
    [SerializeField] private string host = "ws://localhost";
    [SerializeField] private int port = 3000;
    [SerializeField] private string playerHandlerPath = "/player";

    public string Host => host;
    public string PlayerHandlerPath => playerHandlerPath;
    public int Port => port;

    public string URL => $"{Host}:{Port}";
    public string ConnectionURL => URL + PlayerHandlerPath;

}
