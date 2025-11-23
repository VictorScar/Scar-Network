using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NetworkMessageType
{
  Message,
  Connection,
  Disconnection,
  RPC,
  Event,
  SyncField
}
