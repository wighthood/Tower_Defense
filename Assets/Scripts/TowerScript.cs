using System;
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
    private GameObject _Target;

    private void Awake()
    {
        _timer = 0;
        _StartPoint = transform;
        _SpawnTimer = tower._spawnRate;
        _Range = tower._range;
        _AOE = tower._AOE;
        _ProjectilePool = new ComponentPool<ProjectileScript>(projectile, _maxSpawn, _minSpawn);
    }

    private GameObject AcquireTarget()
    {
        List<Collider2D>targets = Physics2D.OverlapCircleAll(_StartPoint.position, _Range)
            .Where(x => x.GetComponent<EnemyScript>() != null).ToList();
        float max = 0;
        GameObject CurrentTarget = null;
        foreach (var target in targets)
        {
            float distance = target.GetComponent<EnemyScript>()._Distance;
            if (distance > max)
            {
                max =distance;
                CurrentTarget = target.gameObject;
            }
        }
        return CurrentTarget;
    }

    private void Update()
    {
        _Target = AcquireTarget();
        if (_Target == null) return;
        _timer += Time.deltaTime;
        if (!(_timer >= _SpawnTimer) ) return;
        _timer = 0.0f;
        CreateNewProjectile();
    }

    private ProjectileScript CreateNewProjectile()
    {
        ProjectileScript projectile = _ProjectilePool.Get();
        projectile.transform.position = _StartPoint.position;
        projectile.target = _Target;

        return projectile;
    }
}
