using System;
using System.Collections;
using System.Collections.Generic;
using ScarToolkit.ScarUI;
using UnityEngine;
using UnityEngine.Serialization;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private UISystem uiSystem;
    private NetworkConnector _networkConnector;

    private MainMenu _mainMenu;

    private void Start()
    {
        uiSystem.Init();
        _networkConnector = NetworkManager.I.NetworkConnector;
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
        _networkConnector.SendNetworkMessage("BOBOB");
    }

    public void HostLocalGame()
    {
        _networkConnector.HostLocalGame();
    }

    public void JoinToLocalGame()
    {
        _networkConnector.JoinToLocalGame();
    }
    
    public void DisconnectFromServer()
    {
        _networkConnector.Disconnect();
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