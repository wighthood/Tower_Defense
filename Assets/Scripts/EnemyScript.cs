using System;
using System.Collections.Generic;
using PoolSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyScript : MonoBehaviour, IPoolableObject<EnemyScript>
{
    [SerializeField] private Enemy _Enemy;
    public List<Transform> _Nodes = new List<Transform>();
    private Transform _Transform;
    public int _PV;
    private float _Speed;
    private int _Attack;
    private int i;
    private Pool<EnemyScript> _Pool;
    public float _Distance;
    
    public Pool<EnemyScript> Pool
    {
        get => _Pool; set => _Pool = value;
    }

    private void OnEnable()
    {
        i = 0;
        _Distance = 0;
        _Transform = transform;
        _PV = _Enemy._PV;
        _Speed = _Enemy._Speed;
        _Attack = _Enemy._Attack;
    }

    // Update is called once per frame
    void Update()
    {
        if (_PV <= 0)
        {
            Pool.Release(this);
        }
        if (_Nodes.Count <= 0 || i >= _Nodes.Count) return;
        if (_Transform.position != _Nodes[i].position)
        {
            _Distance += _Speed * Time.deltaTime;
            _Transform.position = Vector3.MoveTowards(transform.position, _Nodes[i].position, _Speed * Time.deltaTime);
        }
        else
        {
            if (i == _Nodes.Count-1)
            {
                _Nodes[^1].GetComponent<GameManager>()._Lives--;
                Pool.Release(this);
            }
            i++;
        }
    }
}
