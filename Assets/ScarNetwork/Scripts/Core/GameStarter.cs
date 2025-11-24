using ScarNetwork.Scripts.Core;
using ScarToolkit.ScarUI;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private UISystem uiSystem;
    private NetworkManager _networkManager;

    private MainMenu _mainMenu;

    private void Start()
    {
        uiSystem.Init();
        _networkManager = NetworkManager.I;
        var mainMenuScreen = uiSystem.GetScreen<MainMenuScreen>();
        _mainMenu = uiSystem.GetScreen<MainMenuScreen>().MainMenu;

        _mainMenu.StartLocalServerBtn.onClick += HostLocalGame;
        _mainMenu.JoinToLocalServerBtn.onClick += JoinToLocalGame;
        _mainMenu.DisconnectBtn.onClick += DisconnectFromServer;
        _mainMenu.SendMsgBtn.onClick += OnSendMessageClick;
        mainMenuScreen.Show();
        _mainMenu.Show();
    }



    private void OnSendMessageClick()
    {
        _networkManager.Client.SendNetworkMessage("BOBOB");
    }

    public void HostLocalGame()
    {
        _networkManager.StartServer();
    }

    public void JoinToLocalGame()
    {
        _networkManager.JoinToGame();
    }
    
    public void DisconnectFromServer()
    {
        _networkManager.Disconnect();
    }

    private void OnDestroy()
    {
        if (_mainMenu)
        {
            _mainMenu.StartLocalServerBtn.onClick -= HostLocalGame;
            _mainMenu.JoinToLocalServerBtn.onClick -= JoinToLocalGame;
            _mainMenu.DisconnectBtn.onClick -= DisconnectFromServer;
            _mainMenu.SendMsgBtn.onClick -= OnSendMessageClick;
        }
    }
}