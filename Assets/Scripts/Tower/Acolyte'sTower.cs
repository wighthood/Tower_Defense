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
          _ContactFilter.layerMask = LayerMask.GetMask("Ennemy");
          _ContactFilter.useLayerMask = true;
        _ProjectilePool = new ComponentPool<ProjectileScript>(projectile, _maxSpawn, _minSpawn);
    }

    protected override void Process()
    {
        CreateNewProjectile();
    }
    
   private void FindTarget()
    {
        float x = 0;
        Physics2D.OverlapCircle(_StartPoint.position, _Range, _ContactFilter, Collider);
        foreach (var target in Collider) 
        {
            if (target.GetComponent<EnemyScript>()._Distance > x && target.GetComponent<EnemyScript>()._PV > 0)
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
