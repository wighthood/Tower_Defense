using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
    }
    
    public void startGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
}
