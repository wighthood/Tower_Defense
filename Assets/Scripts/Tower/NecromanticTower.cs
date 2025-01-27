using System.Collections.Generic;
using System.Linq;
using PoolSystem;
using UnityEngine;

public class NecromanticTower : StructBase
{
    [SerializeField] private int _maxSpawn;
    [SerializeField] private int _minSpawn;
    [SerializeField] private Transform _Rallypoint;
    [SerializeField] private GameObject _UndeadWarrior;
    [SerializeField] private Undead _Undead;
    
    private ContactFilter2D _ContactFilter2;
    private List<Collider2D> Collider2 = new List<Collider2D>();
    private SlowTrap _Storage;
    private EnemyScript Corpse;
    private ComponentPool<UndeadScript> _UndeadPool;
    private List<UndeadScript> ActiveUndead = new List<UndeadScript>();
    protected override void Awake()
    {
        base.Awake();
        _UndeadPool = new ComponentPool<UndeadScript>(_UndeadWarrior, _maxSpawn, _minSpawn);
    }

    protected override void Process()
    {
        SummonNewUndead();
    }

    private void SpawnUndead()
    {
        UndeadScript newUndead = _UndeadPool.Get(); 
        newUndead.ondeath.AddListener(removeUndead);
        newUndead.transform.position = _Rallypoint.position;
        newUndead._PV = _Undead._PV;
        newUndead._Speed = _Undead._Speed;
        newUndead._Attack = _Undead._Attack;
        newUndead._AttackSpeed = _Undead._AttackSpeed;
        newUndead._AttackRange = _Undead._AttackRange;
        newUndead._LifeTime = _Undead._LifeTime;
        newUndead._Rallypoint = _Rallypoint;
        newUndead._VisionRange = _Undead._VisionRange;
        newUndead._CoolDown = _Undead._Cooldown;
            
        ActiveUndead.Add(newUndead);
    }
    
    private void  SummonNewUndead()
    {
        if (FindCorpseStorage() || FindCorpse())
        {
            if (_Storage != null)
            {
                if (_Storage.Storage > 0)
                {
                   _Storage.Storage--;
                   SpawnUndead();
                   return;
                } 
            }
            if (Corpse != null)
            {
                Corpse.Release();
                Corpse = null;
                SpawnUndead();
            }
        }
    }

    private void removeUndead(UndeadScript undead)
    {
        ActiveUndead.Remove(undead);
    }
    
    private bool FindCorpse()
    {
        _ContactFilter.layerMask = LayerMask.GetMask("Ennemy");
        _ContactFilter.useLayerMask = true;
        Physics2D.OverlapCircle(_StartPoint.position, _Range, _ContactFilter, Collider);
        if (Collider != null)
        {
            List<Collider2D> CorpseCollider = Collider.Where(x => x.GetComponent<EnemyScript>() is not null).ToList();
            foreach (var corpse in CorpseCollider)
            {
                Corpse = corpse.GetComponent<EnemyScript>();
                if (Corpse._IsDead)
                {
                    return true;
                }
            }
        }
        Corpse = null;
        return false;
    }
    private bool FindCorpseStorage()
    { 
        Collider2 = Physics2D.OverlapCircleAll(_StartPoint.position, _Range, 1<<7).ToList();
        if(Collider2.Count == 0) return false;
        _Storage = Collider2[0].GetComponent<SlowTrap>();
        return true;
    }
}