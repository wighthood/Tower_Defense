using PoolSystem;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, Min(0.0f)] private float _spawnRate;
    [SerializeField] private Transform _StartPoint;
    [SerializeField] private Transform[] Nodes;
    [SerializeField] private Transform _EndPoint;
    [SerializeField] private GameObject _Enemy;
    [SerializeField] private int _maxSpawn;
    [SerializeField] private int _minSpawn;
    
    private ComponentPool<EnemyScript> _EnemyPool;
    private float _timer;
    void Start()
    {
        _EnemyPool = new ComponentPool<EnemyScript>(_Enemy, _maxSpawn, _minSpawn);
        _timer = 0.0f;
    }
    
    public EnemyScript CreateNewEnemy()
    {
        EnemyScript enemy = _EnemyPool.Get();
        enemy.GetComponent<EnemyScript>()._Nodes.Clear();
        foreach(var node in Nodes)
        {
            enemy.GetComponent<EnemyScript>()._Nodes.Add(node);
        }
        enemy.GetComponent<EnemyScript>()._Nodes.Add(_EndPoint);
        enemy.transform.position = _StartPoint.position;
        return enemy;
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnRate)
        {
            _timer = 0.0f;
            CreateNewEnemy();
        }
    }
}
