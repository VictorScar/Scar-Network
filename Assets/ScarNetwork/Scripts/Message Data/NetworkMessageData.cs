using System;
using System.Collections;
using System.Collections.Generic;
using ScarNetwork.Scripts.Message_Data;
using UnityEngine;

[Serializable]
public struct NetworkMessageData
{
   public NetworkMessageType MessgeType;
   public string Data;
}

[Serializable]
public struct ObjectMessageData
{
   public int ObjectID;
   public string Data;
}

[Serializable]
public struct TransportMessageData
{
   public NetworkMessageTarget Target;
}

[Serializable]
public struct ConnectionMessageData
{
   public string ClientID;
   public string NickName;
}