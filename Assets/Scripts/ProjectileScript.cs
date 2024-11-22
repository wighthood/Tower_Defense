using PoolSystem;
using UnityEngine;

public class ProjectileScript : MonoBehaviour, IPoolableObject<ProjectileScript>
{
    public Pool<ProjectileScript> Pool { get; set; }
}
