using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork
{
    private string _nickName;
    private string _id;

    public string ID => _id;

    public void SetID(string id)
    {
        _id = id;
    }

    public string NickName
    {
        get => _nickName;
        set => _nickName = value;
    }
}
