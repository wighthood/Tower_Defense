using PoolSystem;
using UnityEngine;

public class NecromanticTower : StructBase
{
    [SerializeField] private int _maxSpawn;
    [SerializeField] private int _minSpawn;
    private ComponentPool<ProjectileScript> _UndeadPool;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Process()
    {
        base.Process();
        if ((_timer <= _SpawnTimer)) return;
        _timer = 0.0f;
    }
}
