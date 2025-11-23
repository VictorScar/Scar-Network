using System.Collections;
using System.Collections.Generic;
using ScarToolkit.ScarUI;
using UnityEngine;

public class MainMenuScreen : UIScreen
{
    [SerializeField] private MainMenu mainMenu;

    public MainMenu MainMenu => mainMenu;
    protected override void OnInit()
    {
        base.OnInit();
        mainMenu.Init();
    }
}
