using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float _Lives;

    private void Update()
    {
        if (_Lives > 0) return;
        Application.Quit();
    }
}
