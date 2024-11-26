using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float _Lives;
    public int _Money;
  
    private void Update()
    {
        if (_Lives > 0) return;
        Application.Quit();
    }
}
