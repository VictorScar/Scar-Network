using System.Collections;
using System.Collections.Generic;
using ScarNetwork.Scriptes.Network_Object;
using UnityEngine;

public class NetworkObjectStorage : MonoBehaviour
{
   private List<NetworkObject> _netObjects = new List<NetworkObject>();
   
   public void Init()
   {
      Debug.Log("Storage Init");
      _netObjects.Clear();
     }
   
   public void RegisterObject(NetworkObject nObj)
   {
      nObj.SetID(_netObjects.Count);
      _netObjects.Add(nObj);
      Debug.Log("Register");
   }

   public void UnregisterObject(NetworkObject nObj)
   {
      _netObjects.Remove(nObj);
   }

   public NetworkObject GetObjectByID(int id)
   {
      if (id >= 0 && id < _netObjects.Count)
      {
         return _netObjects[id];
      }

      return null;
   }
}
