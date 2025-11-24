using WebSocketSharp;

namespace ScarNetwork.Scripts.Server
{
    public class ServerNetworkClient
    {
        private string _nickName;
        private string _clientID;
        private string _playerData;
        private WebSocket _socket;

        public string NickName
        {
            get => _nickName;
            set => _nickName = value;
        }

        public string ClientID => _clientID;
        public WebSocket Socket => _socket;
    
        public ServerNetworkClient(NetworkClientData clientData)
        {
            _nickName = clientData.NickName;
            _clientID = clientData.ClientID;
            _playerData = clientData.PlayerData;
            _socket = clientData.Socket;
        }
    }

    public struct NetworkClientData
    {
        public string NickName;
        public string ClientID;
        public string PlayerData;
        public WebSocket Socket;
    }
}