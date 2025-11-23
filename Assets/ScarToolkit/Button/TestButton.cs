using System.Collections;
using System.Collections.Generic;
using ScarToolkit.Button;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    [Button("Test")]
    public void TestBtn()
    {
        Debug.Log("TEST!!!!");
    }
}
