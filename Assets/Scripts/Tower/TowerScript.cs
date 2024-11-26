using PoolSystem;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private Tower tower;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int _maxSpawn;
    [SerializeField] private int _minSpawn;
    [SerializeField] GameObject _RangeCollider;
    
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
        _RangeCollider.GetComponent<CircleCollider2D>().radius = tower._range;
    }
    private void Update()
    {
        if (_timer >= _SpawnTimer  ) return;
        _timer += Time.deltaTime;
        if ((_timer <= _SpawnTimer) ) return;
        _timer = 0.0f;
        CreateNewProjectile();
    }
    
    private ProjectileScript CreateNewProjectile()
    {
        _Target = _RangeCollider.GetComponent<FindTarget>().GetTarget();
        if (_Target == null) return null;
        ProjectileScript projectile = _ProjectilePool.Get(); 
        projectile.transform.position = _StartPoint.position;
        projectile.target = _Target;
        

        return projectile;
    }
}
