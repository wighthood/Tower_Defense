using System.Collections.Generic;
using PoolSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour, IPoolableObject<EnemyScript>
{
    [SerializeField] private Sprite _DeadSprite;
    [SerializeField] private Image _HPBar;
    public List<Transform> _Nodes = new List<Transform>();
    private Transform _Transform;
    public int _PV;
    public int MaxHp;
    public int _Prime;
    public float _Distance { get;private set; }
    public float _Speed;
    public int _Attack;
    public float _SpeedMultiplier = 1f;
    public float _AttackSpeed;
    private int i;
    private float DecayTimer;
    private float CoolDownTimer;
    [SerializeField] float DecayTime;
    private Pool<EnemyScript> _Pool;
    public bool _IsDead;
    public GameManager _GameManager;
    public UndeadScript _Undead;

    public UnityEvent<EnemyScript> ondeath;
    public Pool<EnemyScript> Pool
    {
        get => _Pool; set => _Pool = value;
    }

    private void OnEnable()
    {
        i = 0;
        _SpeedMultiplier = 1f;
        _Distance = 0;
        _Transform = transform;
    }

    private void behavior()
    {
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
                _Nodes.Clear();
                Pool.Release(this);
            }
            i++;
        }
        if (_Undead != null)
        {
            CoolDownTimer += Time.deltaTime * _AttackSpeed;
            if (CoolDownTimer >= 1)
            {
                CoolDownTimer = 0;
                _Undead._PV -= _Attack;
            }
        }
        else
        {
            _SpeedMultiplier = 1f;
        }
    }

    private void death()
    {
        i = 0;
        _IsDead = true;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _DeadSprite;
        _GameManager._Money += _Prime;
        _Nodes.Clear();
        _GameManager.updateMoneyText();
        ondeath.Invoke(this);
    }

    public void Release()
    {
        Pool.Release(this);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_Undead == null || !_Undead.isActiveAndEnabled)  _SpeedMultiplier = 1f;
        _HPBar.fillAmount =(float) _PV/MaxHp;
        if (!_IsDead)
        {
            if (_PV <= 0)
            {
                death();
            }
            else
            {
                behavior();
            }
        }
        else
        {
           DecayTimer += Time.deltaTime;
            if (DecayTimer >= DecayTime)
            {
                DecayTimer = 0;
                Release();
            }
        }
    }
}

