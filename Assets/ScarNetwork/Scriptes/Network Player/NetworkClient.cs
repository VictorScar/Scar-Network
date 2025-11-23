using WebSocketSharp;

namespace ScarNetwork.Scriptes.Network_Player
{
    public class NetworkClient
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
    
        public NetworkClient(NetworkClientData clientData)
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