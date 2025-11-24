using System.Collections;
using System.Collections.Generic;
using ScarToolkit.ScarUI;
using UnityEngine;

public class MainMenu : UIView
{
    [SerializeField] private UIClickableView startLocalServerBtn;
    [SerializeField] private UIClickableView joinToLocalServerBtn;
    [SerializeField] private UIClickableView sendMsgBtn;
    [SerializeField] private UIClickableView disconnectBtn;

    public UIClickableView StartLocalServerBtn => startLocalServerBtn;
    public UIClickableView JoinToLocalServerBtn => joinToLocalServerBtn;
    public UIClickableView SendMsgBtn => sendMsgBtn;
    public UIClickableView DisconnectBtn => disconnectBtn;

    protected override void OnInit()
    {
        base.OnInit();
        startLocalServerBtn.Init();
        joinToLocalServerBtn.Init();
        startLocalServerBtn.Init();
        disconnectBtn.Init();
    }
}
