namespace ScarNetwork.Scripts.Server
{
    public interface IServer
    {
        void StartServer(ServerSettings settings);
        void StopServer();
        public bool IsStarted { get; }
        PlayerNetwork[] GetPlayers();
        
    }
}
