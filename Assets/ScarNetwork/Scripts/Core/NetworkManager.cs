using ScarNetwork.Scripts.Server;
using UnityEngine;

namespace ScarNetwork.Scripts.Core
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField] private ServerSettings serverSettings;
        [SerializeField] private NetworkObjectStorage netObjStorage;

        private IServer _server;
        private NetworkClient _client;
        private PlayerNetwork _localPlayer;

        public bool IsMaster => _server != null && _server.IsStarted;

        public static NetworkManager I { get; private set; }

        public NetworkClient Client => _client;

        public NetworkObjectStorage NetObjectStorage => netObjStorage;
        public bool IsConnected => _client.IsConnected;

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
            _client = new NetworkClient(netObjStorage, _localPlayer);
            netObjStorage.Init();
        }

        private void OnDestroy()
        {
            if (_server != null)
            {
                _server.StopServer();
            }

            Disconnect();
        }

        public void StartServer()
        {
            _server = new LocalGameServer();
            _server.StartServer(serverSettings);
        }

        public void JoinToGame()
        {
            _client.JoinToServer(serverSettings.ConnectionURL);
        }

        public void Disconnect()
        {
            if (_client != null)
            {
                _client.Disconnect();
            }
        }

        public bool IsMine(string ownerID)
        {
            return ownerID == _localPlayer.ID;
        }
    }
}