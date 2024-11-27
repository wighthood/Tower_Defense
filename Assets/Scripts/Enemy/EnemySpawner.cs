using System.Collections.Generic;
using PoolSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyWithQuantity> _Enemies;
    public  int count;
    public List<EnemyScript> _AliveEnemies { private set; get; } = new();
    
    [ Min(0.0f)] public float _spawnRate;
    [SerializeField] private Transform[] Nodes;
    [SerializeField] private Transform _EndPoint;
    [SerializeField] private int _maxSpawn;
    [SerializeField] private int _minSpawn;
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private GameObject _Enemy;
    //private Dictionary<EnemyType,Pool<EnemyScript>> _Pools = new ();
    private ComponentPool<EnemyScript> _EnemyPool;
    private float _timer;
    private Transform _StartPoint;
    void OnEnable()
    {
        _StartPoint = transform;
        //_Pools.Clear();
       /* foreach (var enemy in _Enemies)
        {
            if (_Pools.ContainsKey(enemy.Enemy.Type)) continue;
            _Pools[enemy.Enemy.Type] = new ComponentPool<EnemyScript>(enemy.Enemy,_maxSpawn,_spawnRate);
        }*/
        _EnemyPool = new ComponentPool<EnemyScript>(_Enemy, _maxSpawn, _minSpawn);
        _timer = 0.0f;
    }
    
    public EnemyScript CreateNewEnemy()
    {
        count++;
        int EnemyToSpawn = Random.Range(0, _Enemies.Count);
        EnemyScript enemy = _EnemyPool.Get();
        enemy.ondeath.AddListener(RemoveEnnemy);
        enemy._Nodes.Clear();
        foreach (var node in Nodes)
        {
            enemy._Nodes.Add(node);
        }
        enemy._Nodes.Add(_EndPoint);
        enemy._GameManager = _GameManager;
        enemy.transform.position = _StartPoint.position;
        enemy._PV = _Enemies[EnemyToSpawn].Enemy._PV;
        enemy._Attack = _Enemies[EnemyToSpawn].Enemy._Attack;
        enemy._Speed = _Enemies[EnemyToSpawn].Enemy._Speed;
        enemy._Prime = _Enemies[EnemyToSpawn].Enemy._Prime;
        _AliveEnemies.Add(enemy);
        return enemy;
    }
    
    private void RemoveEnnemy(EnemyScript enemy)
    {
        _AliveEnemies.Remove(enemy);
    }

    
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnRate && count <  _Enemies[0].Quantity)
        {
            _timer = 0.0f;
            CreateNewEnemy();
        }
    }
}

public enum  EnemyType
{
    normal
}
