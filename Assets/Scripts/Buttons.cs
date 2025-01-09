using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void resumeGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void startGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
}
