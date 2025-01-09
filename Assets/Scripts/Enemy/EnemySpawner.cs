using System.Collections.Generic;
using PoolSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyWithQuantity> _Enemies;
    public List<int> _Counts;
    public List<EnemyScript> _AliveEnemies { private set; get; } = new();
    [ Min(0.0f)] public float _spawnRate;
    [SerializeField] private Transform[] Nodes;
    [SerializeField] private Transform _EndPoint;
    [SerializeField] private int _maxSpawn;
    [SerializeField] private int _minSpawn;
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private GameObject _Enemy;
    private ComponentPool<EnemyScript> _EnemyPool;
    private float _timer;
    private Transform _StartPoint;
    private int EnemyToSpawn;
    void OnEnable()
    {
        _StartPoint = transform;
        _EnemyPool = new ComponentPool<EnemyScript>(_Enemy, _maxSpawn, _minSpawn);
        _timer = 0.0f;
    }
    
    public EnemyScript CreateNewEnemy()
    {
        for (int i = 0; i < _Enemies.Count; i++)
        {
            EnemyToSpawn = Random.Range(0, _Enemies.Count);
            if (_Counts[EnemyToSpawn] >= _Enemies[EnemyToSpawn].Quantity)
            {
                _Enemies.RemoveAt(EnemyToSpawn);
                _Counts.RemoveAt(EnemyToSpawn);
                continue;
            }
            _Counts[EnemyToSpawn]++;
            EnemyScript enemy = _EnemyPool.Get();
            enemy.ondeath.RemoveAllListeners();
            enemy.ondeath.AddListener(RemoveEnnemy);
            enemy._Nodes.Clear();
            foreach (var node in Nodes)
            {
                enemy._Nodes.Add(node);
            }
            enemy._IsDead = false;
            enemy._Nodes.Add(_EndPoint);
            enemy._GameManager = _GameManager;
            enemy.transform.position = _StartPoint.position;
            enemy._PV = _Enemies[EnemyToSpawn].Enemy._PV;
            enemy._Attack = _Enemies[EnemyToSpawn].Enemy._Attack;
            enemy._Speed = _Enemies[EnemyToSpawn].Enemy._Speed;
            enemy._Prime = _Enemies[EnemyToSpawn].Enemy._Prime;
            enemy._AttackSpeed = _Enemies[EnemyToSpawn].Enemy._AttackSpeed;
            enemy._SpeedMultiplier = 1f;
            enemy.GetComponentInChildren<SpriteRenderer>().sprite = _Enemies[EnemyToSpawn].Enemy._Sprite;
            _AliveEnemies.Add(enemy);
            return enemy;
        }
        return null;
    }
    
    private void RemoveEnnemy(EnemyScript enemy)
    {
        _AliveEnemies.Remove(enemy);
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnRate  && _Enemies.Count > 0)
        {
              _timer = 0.0f;
            CreateNewEnemy();
        }
    }
}