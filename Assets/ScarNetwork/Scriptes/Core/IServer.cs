namespace ScarNetwork.Scriptes.Core
{
    public interface IServer
    {
        void StartServer(LocalServerSettings settings);
        void StopServer();
    }
}
