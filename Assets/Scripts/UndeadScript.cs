using System.Collections.Generic;
using PoolSystem;
using UnityEngine;
using UnityEngine.Events;

public class UndeadScript : MonoBehaviour , IPoolableObject<UndeadScript>
{
    public int _PV;
    public float _Speed;
    public int _Attack;
    public float _AttackSpeed;
    public float _AttackRange;
    public float _LifeTime;
    public float _VisionRange;
    public float _CoolDown;
    public Transform _Rallypoint;
    public Pool<UndeadScript> UndeadPool { get; set; }
    public UnityEvent<UndeadScript> ondeath;
    private float _LifeTimer;
    private EnemyScript _target;
    private ContactFilter2D _ContactFilter;
    private List<Collider2D> Collider = new List<Collider2D>();
    private float _TargettingTimer ;
    private float _AttackDelay;
    public Pool<UndeadScript> Pool
    {
        get => UndeadPool; set => UndeadPool = value;
    }

    private void Awake()
    {
        _ContactFilter.layerMask = LayerMask.GetMask("Ennemy");
        _ContactFilter.useLayerMask = true;
    }

    private void LifeTime()
    {
            _LifeTimer = 0;
            Death();
    }

    private void attack()
    {
        Vector3 TargetDistance = _target.transform.position - transform.position;
        if (TargetDistance.magnitude < _AttackRange)
        {
            if (_target._Undead == null)
            {
                _target._Undead = this;
            }
            _AttackDelay += Time.deltaTime * _AttackSpeed;
            if (_AttackDelay > 1)
            {
                _AttackDelay = 0;
                _target._PV -= _Attack;
            }
        }
    }
    
    private void Behaviour()
    {
        if (_target == null)
        {
            if (_TargettingTimer >= _CoolDown)
            {
                _TargettingTimer = 0;
                if (findTarget())
                {
                    if (_target == null)return;
                    _target.ondeath.AddListener(RemoveTarget);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _Rallypoint.position, _Speed * Time.deltaTime);
            }
        }
        else
        {
            _target._SpeedMultiplier = 0;
            attack();
            Vector3 distance = _target.transform.position - _Rallypoint.position;
            if (distance.magnitude < _VisionRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.gameObject.transform.position,
                    _Speed * Time.deltaTime);
            }
            else
            {
                _target = null;
            }
        }
    }

    private void RemoveTarget(EnemyScript target)
    {
        _target = null;
    }
    private void Update()
    {   
        _LifeTimer += Time.deltaTime;
        _TargettingTimer += Time.deltaTime;
        if (_LifeTimer >= _LifeTime || _PV <= 0)
        {
            LifeTime();
        }
        Behaviour();
    }
    
    private bool findTarget()
    {
        float x = 0;
        Physics2D.OverlapCircle(_Rallypoint.position, _VisionRange, _ContactFilter, Collider);
        if (Collider.Count == 0)
        {
            return false;
        }
        foreach (var target in Collider) 
        {
            if (target.GetComponent<EnemyScript>()._Distance > x && target.GetComponent<EnemyScript>()._PV > 0)
            {
                x = target.GetComponent<EnemyScript>()._Distance;
                _target = target.gameObject.GetComponent<EnemyScript>();
            }
        }
        return true;
    }
    
    private void Death()
    {
        if (_target != null)
        {
            _target._Undead = null;
            _target.ondeath.RemoveListener(RemoveTarget);
            _target._SpeedMultiplier = 1f;
            _target = null;
        }
        ondeath.Invoke(this);
        Pool.Release(this);
    }
    
}
