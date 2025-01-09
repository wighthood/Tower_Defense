using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private List<Level> levels;
    [SerializeField] private float WaveDelay;
    [SerializeField] private TextMeshProUGUI _GameOverText;
    [SerializeField] private GameObject _GameOverPanel;

    
    private float Timer;
    private int currentLevel = 0;
    
    private void Update()
    {
        if (spawner._Enemies.Count == 0 && spawner._AliveEnemies.Count ==0)
        {
            Timer += Time.deltaTime;
        }

        if (Timer >= WaveDelay)
        {
            Timer = 0;
            if (currentLevel  <  levels.Count) LoadLevel();
            else
            {
                Time.timeScale = 0;
                _GameOverText.SetText("you won");
                _GameOverPanel.SetActive(true);
            }
        }
    }

    private void LoadLevel()
    {
        foreach (var enemy in levels[currentLevel].enemies)
        {
            spawner._Counts.Add(0);
        }
        spawner._Enemies = new List<EnemyWithQuantity>(levels[currentLevel].enemies);
        spawner._spawnRate = levels[currentLevel].spawnrate;
        currentLevel++;
       // Debug.Log(currentLevel);
    }
}
