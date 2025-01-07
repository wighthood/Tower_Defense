using PoolSystem;
using UnityEngine;

public class ProjectileScript : MonoBehaviour, IPoolableObject<ProjectileScript>
{
    [SerializeField] private Projectile projectile;
    private Transform _transform;
    private float _speed;
    private int _damage;
    public GameObject target;
    public Pool<ProjectileScript> Pool { get; set; }

    private void OnEnable()
    {
        _speed = projectile.speed;
        _damage = projectile.damage;
        _transform = this.transform;
    }

    private void DealDamage()
    {
        target.GetComponent<EnemyScript>()._PV -= _damage;
        Pool.Release(this);
    }

    private void Update()
    {
        if (target == null) return;
        if (target.GetComponent<EnemyScript>()._IsDead)
        {
            Pool.Release(this);
        }
        _transform.position = Vector3.MoveTowards(_transform.position, target.transform.position, _speed * Time.deltaTime);
        if (_transform.position != target.transform.position) return;
        DealDamage();
    }
}
