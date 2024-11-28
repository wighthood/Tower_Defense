using System.Collections.Generic;
using PoolSystem;
using UnityEngine;
using UnityEngine.Events;

public class EnemyScript : MonoBehaviour, IPoolableObject<EnemyScript>
{
    public List<Transform> _Nodes = new List<Transform>();
    private Transform _Transform;
    public int _PV;
    public int _Prime;
    public float _Distance { get;private set; }
    public float _Speed;
    public int _Attack;
    public float _SpeedMultiplier = 1f;
    private int i;
    private Pool<EnemyScript> _Pool;
    public GameManager _GameManager;

    public UnityEvent<EnemyScript> ondeath;
    public Pool<EnemyScript> Pool
    {
        get => _Pool; set => _Pool = value;
    }

    private void OnEnable()
    {
        i = 0;
        _Distance = 0;
        _Transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_PV <= 0)
        {
            _GameManager._Money += _Prime;
            _GameManager.updateMoneyText();
            ondeath.Invoke(this);
            Pool.Release(this);
        }
        if (_Nodes.Count <= 0 || i >= _Nodes.Count) return;
        if (_Transform.position != _Nodes[i].position)
        {
            _Distance += _Speed * Time.deltaTime;
            _Transform.position = Vector3.MoveTowards(transform.position, _Nodes[i].position, _Speed * Time.deltaTime*_SpeedMultiplier);
        }
        else
        {
            if (i == _Nodes.Count-1)
            {
                _Nodes[^1].GetComponent<GameManager>()._Lives--;
                _Nodes[^1].GetComponent<GameManager>().updateLivesText();
                Pool.Release(this);
            }
            i++;
        }
    }
}
