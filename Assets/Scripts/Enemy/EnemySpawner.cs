using PoolSystem;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform _StartPoint;
    [SerializeField, Min(0.0f)] private float _spawnRate;
    [SerializeField] private Transform[] Nodes;
    [SerializeField] private Transform _EndPoint;
    [SerializeField] private GameObject _Enemy;
    [SerializeField] private int _maxSpawn;
    [SerializeField] private int _minSpawn;
    [SerializeField] private GameManager _GameManager;
    
    private ComponentPool<EnemyScript> _EnemyPool;
    private float _timer;
    void Start()
    {
        _StartPoint = transform;
        _EnemyPool = new ComponentPool<EnemyScript>(_Enemy, _maxSpawn, _minSpawn);
        _timer = 0.0f;
    }
    
    public EnemyScript CreateNewEnemy()
    {
        EnemyScript enemy = _EnemyPool.Get();
        enemy._Nodes.Clear();
        foreach(var node in Nodes)
        {
            enemy._Nodes.Add(node);
        }
        enemy._Nodes.Add(_EndPoint);
        enemy._GameManager = _GameManager;
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
