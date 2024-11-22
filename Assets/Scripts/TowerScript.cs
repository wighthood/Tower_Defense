using System;
using System.Collections.Generic;
using PoolSystem;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private Tower tower;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int _maxSpawn;
    [SerializeField] private int _minSpawn;

    private List<Collider2D> targets;
    private Transform _StartPoint;
    private Transform _EndPoint;
    private ComponentPool<ProjectileScript> _ProjectilePool;
    private float _SpawnTimer;
    private float _Range;
    private float _timer;
    private bool _AOE;

    private void Awake()
    {
        _timer = 0;
        _StartPoint = transform;
        _SpawnTimer = tower._spawnRate;
        _Range = tower._range;
        _AOE = tower._AOE;
    }

    private bool AcquireTarget()
    {
        targets = new List<Collider2D> { Physics2D.OverlapCircle(_StartPoint.position, _Range) };
        return true;
    }
    
    private void Update()
    {
        if (!AcquireTarget()) return;
        _timer += Time.deltaTime;
        if (!(_timer >= _SpawnTimer)) return;
        _timer = 0.0f;
        CreateNewProjectile();
    }

    private void CreateNewProjectile()
    {
        throw new NotImplementedException();
    }
}
