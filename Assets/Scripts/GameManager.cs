using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public float _Lives;
  
    private void Update()
    {
        if (_Lives > 0) return;
        Application.Quit();
    }
}
