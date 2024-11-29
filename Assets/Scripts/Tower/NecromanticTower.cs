using System.Collections.Generic;
using System.Linq;
using PoolSystem;
using UnityEngine;

public class NecromanticTower : StructBase
{
    [SerializeField] private int _maxSpawn;
    [SerializeField] private int _minSpawn;
    [SerializeField] private GameObject _Rallypoint;
    [SerializeField] private GameObject _UndeadWarrior;
    private GameObject _Storage;
    private ComponentPool<ProjectileScript> _UndeadPool;

    protected override void Awake()
    {
        base.Awake();
        _UndeadPool = new ComponentPool<ProjectileScript>(_UndeadWarrior, _maxSpawn, _minSpawn);
    }

    protected override void Process()
    {
        SummonNewUndead();
    }

    private void  SummonNewUndead()
    {
        if (FindCorpseStorage())
        { 
            Debug.Log("Summonned undead");
        }
    }

    private bool FindCorpse()
    {
        return false;
    }
    private bool FindCorpseStorage()
    {
        List<Collider2D> StorageCollider = Physics2D.OverlapCircleAll(_StartPoint.position, _Range).Where(x => x.GetComponent<SlowTrap>() is not null).ToList();
        _Storage = StorageCollider[0].gameObject;
        return true;
    }
}