using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private InputAction PauseKey;

    private void OnEnable()
    {
        PauseKey.Enable();
        PauseKey.performed += Pause;
    }

    private void OnDisable()
    {
        PauseKey.performed -= Pause;
        PauseKey.Disable();
    }

    private void Pause(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        PauseMenu.SetActive(!PauseMenu.activeSelf);
    }
    
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
