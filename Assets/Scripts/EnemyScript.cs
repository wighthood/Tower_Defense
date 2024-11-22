using System.Collections.Generic;
using PoolSystem;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IPoolableObject<EnemyScript>
{
    [SerializeField] private Enemy _Enemy;
    public List<Transform> _Nodes = new List<Transform>();
    private Transform _Transform;
    private int _PV;
    private float _Speed;
    private int _Attack;
    private int i;
    private Pool<EnemyScript> _Pool;
    
    public Pool<EnemyScript> Pool
    {
        get => _Pool; set => _Pool = value;
    }

    private void OnEnable()
    {
        i = 0;
        _Transform = transform;
        _PV = _Enemy._PV;
        _Speed = _Enemy._Speed;
        _Attack = _Enemy._Attack;
    }

    // Update is called once per frame
    void Update()
    {
        if (_Nodes.Count <= 0 || i >= _Nodes.Count) return;
        if (_Transform.position != _Nodes[i].position)
        {
            _Transform.position = Vector3.MoveTowards(transform.position, _Nodes[i].position, _Speed * Time.deltaTime);
        }
        else
        {
            if (i == _Nodes.Count-1)
            {
                Pool.Release(this);
            }
            i++;
        }
    }
}
