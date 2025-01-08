using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlowTrap  : StructBase
{
    [SerializeField] private float multiplier = .75f;
    [SerializeField] private int Capacity;
    public int Storage;

    protected override void Awake()
    {
        base.Awake();
        _ContactFilter.layerMask = LayerMask.GetMask("Ennemy");
    }

    protected override void Process()
    {
        if (Storage < Capacity)
        {
            Physics2D.OverlapCircle(_StartPoint.position, _Range, _ContactFilter, Collider);
            if (Collider != null)
            {
                List<Collider2D> Targets = Physics2D.OverlapCircleAll(_StartPoint.position, _Range)
                    .Where(x => x.GetComponent<EnemyScript>() is not null).ToList();
                foreach (Collider2D Target in Targets)
                {
                    if (!Target.gameObject.GetComponent<EnemyScript>()._IsDead) continue;
                    Target.gameObject.GetComponent<EnemyScript>().Release();
                    Storage++;
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyScript>()._SpeedMultiplier > multiplier)
                other.GetComponent<EnemyScript>()._SpeedMultiplier =  multiplier;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyScript>()._SpeedMultiplier = 1f;
        }
    }
}
