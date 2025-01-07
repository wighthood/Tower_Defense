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
    public float _LifeTime;
    public float _VisionRange;
    public float _CoolDown;
    public Transform _Rallypoint;
    public Pool<UndeadScript> UndeadPool { get; set; }
    public UnityEvent<UndeadScript> ondeath;
    private float _LifeTimer = 0;
    private EnemyScript _target;
    private ContactFilter2D _ContactFilter;
    private List<Collider2D> Collider = new List<Collider2D>();
    private float _TargettingTimer = 0;
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
    private void Update()
    {
        _LifeTimer += Time.deltaTime;
        _TargettingTimer += Time.deltaTime;
        if (_LifeTimer >= _LifeTime || _PV <= 0)
        {
            LifeTime();
        }
        
        if (_target == null && _TargettingTimer >= _CoolDown)
        {
            _TargettingTimer = 0;
            if (findTarget())
            {
                
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _Rallypoint.position, _Speed * Time.deltaTime);
            }
        }
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
        ondeath.Invoke(this);
        Pool.Release(this);
    }
    
}
