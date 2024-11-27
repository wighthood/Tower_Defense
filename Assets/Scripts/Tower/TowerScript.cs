using System.Collections.Generic;
using System.Linq;
using PoolSystem;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private Tower tower;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int _maxSpawn;
    [SerializeField] private int _minSpawn;
    
    private Transform _StartPoint;
    private Transform _EndPoint;
    private ComponentPool<ProjectileScript> _ProjectilePool;
    private float _SpawnTimer;
    private float _Range;
    private float _timer;
    private bool _AOE;
    public int _price { get;private  set; }
    private GameObject _Target;

    private void Awake()
    {
        _timer = 0;
        _StartPoint = transform;
        _SpawnTimer = tower._spawnRate;
        _Range = tower._range;
        _ProjectilePool = new ComponentPool<ProjectileScript>(projectile, _maxSpawn, _minSpawn);
        _price = tower._price;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if ((_timer <= _SpawnTimer) ) return;
        _timer = 0.0f;
        FindTarget();
        if (_Target != null) 
            CreateNewProjectile();
    }

    private void FindTarget()
    {
        float x = 0;
        List<Collider2D> Targets = Physics2D.OverlapCircleAll(_StartPoint.position, _Range).Where(x =>x.GetComponent<EnemyScript>() is not null).ToList();
        foreach (var target in Targets)
        {
            if (target.GetComponent<EnemyScript>()._Distance > x)
            {
                x = target.GetComponent<EnemyScript>()._Distance;
                _Target = target.gameObject;
            }
        }
    }
    private ProjectileScript CreateNewProjectile()
    {
        ProjectileScript newProjectile = _ProjectilePool.Get(); 
        newProjectile.transform.position = _StartPoint.position;
        newProjectile.target = _Target;
        return newProjectile;
    }
}
