using PoolSystem;
using UnityEngine;
using UnityEngine.Events;

public class UndeadScript : MonoBehaviour , IPoolableObject<UndeadScript>
{
    public int _PV;
    public float _Speed;
    public int _Attack;
    public float _AttackSpeed;
    public Transform _Rallypoint;
    public Pool<UndeadScript> UndeadPool { get; set; }
    public UnityEvent<UndeadScript> ondeath;
    public float _LifeTime;
    private float _LifeTimer = 0;
    
    public Pool<UndeadScript> Pool
    {
        get => UndeadPool; set => UndeadPool = value;
    }
    private void Update()
    {
        _LifeTimer += Time.deltaTime;
        if (_LifeTimer >= _LifeTime || _PV<=0)
        {
            _LifeTimer = 0;
            UndeadPool.Release(this);
        }
    }
}
