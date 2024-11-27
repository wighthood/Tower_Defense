using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private List<Level> levels;
    [SerializeField] private float WaveDelay;
    
    private float Timer;
    private int currentLevel = 0;

    private void Awake()
    {
        LoadLevel();
    }

    private void Update()
    {
        if (spawner.count >= spawner._Enemies[0].Quantity && spawner._AliveEnemies.Count ==0)
        {
            Timer += Time.deltaTime;
        }

        if (Timer >= WaveDelay)
        {
            currentLevel++;
            if (currentLevel <  levels.Count) LoadLevel();
            Application.Quit();
        }
        
    }

    private void LoadLevel()
    {
        Timer = 0;
        spawner._Enemies = levels[currentLevel].enemies;
        spawner._spawnRate = levels[currentLevel].spawnrate;
        spawner.count = 0;
    }
}
