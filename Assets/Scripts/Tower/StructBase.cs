using System.Collections.Generic;
using UnityEngine;

public  class  StructBase : MonoBehaviour
{
    [SerializeField] private Tower tower;
    protected float _Cooldown;
    protected float _Range;
    protected float _timer;
    protected Transform _StartPoint;
    protected ContactFilter2D _ContactFilter;
    protected List<Collider2D> Collider = new List<Collider2D>();
    public int _price { get;private  set; }
    public bool _IsTrap { get; private set; }
   

    protected virtual void Awake()
    {
        _timer = 0;
        _Cooldown = tower._Cooldown;
        _Range = tower._range;
        _price = tower._price;
        _IsTrap = tower._IsTrap;
        _StartPoint = this.transform;
        _ContactFilter.useLayerMask = true;
    }

    protected void Update()
    {
        _timer += Time.deltaTime;
        if ((_timer <= _Cooldown)) return;
        _timer = 0.0f;
        Process();
    }

    protected virtual void Process() { }
    
}
