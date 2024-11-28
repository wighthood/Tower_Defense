using UnityEngine;

public class SlowTrap  : TowerBase
{
    [SerializeField] private float multiplier = .75f;
    protected override void Process()
    {
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyScript>()._SpeedMultiplier > multiplier)
            other.GetComponent<EnemyScript>()._SpeedMultiplier =  multiplier;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<EnemyScript>()._SpeedMultiplier = 1f;
    }
}
