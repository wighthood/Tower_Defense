using System.Collections.Generic;
using System.Linq;
using PoolSystem;
using UnityEngine;

public class AcolytesStruct : StructBase
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private int _maxSpawn;
    [SerializeField] private int _minSpawn;
    private ComponentPool<ProjectileScript> _ProjectilePool;
    
    private Transform _EndPoint;
    private GameObject _Target;
    protected override void Awake()
    {
        base.Awake();
        _ProjectilePool = new ComponentPool<ProjectileScript>(projectile, _maxSpawn, _minSpawn);
    }

    protected override void Process()
    {
        base.Process();
        if ((_timer <= _SpawnTimer) ) return;
        _timer = 0.0f;
        CreateNewProjectile();
    }
    
   private void FindTarget()
    {
        float x = 0;
        List<Collider2D> Targets = Physics2D.OverlapCircleAll(_StartPoint.position, _Range).Where(x =>x.GetComponent<EnemyScript>() is not null).ToList();
        foreach (var target in Targets)
        {
            if (target.GetComponent<EnemyScript>()._Distance > x && target.GetComponent<EnemyScript>()._PV >0)
            {
                x = target.GetComponent<EnemyScript>()._Distance;
                _Target = target.gameObject;
            }
        }
    }
    private ProjectileScript CreateNewProjectile()
    {
        FindTarget();
        if (_Target == null) return null;
        ProjectileScript newProjectile = _ProjectilePool.Get(); 
        newProjectile.transform.position = _StartPoint.position;
        newProjectile.target = _Target;
        _Target = null;
        return newProjectile;
    }
}
