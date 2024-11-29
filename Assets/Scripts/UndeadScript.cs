using PoolSystem;
using UnityEngine;

public class UndeadScript : MonoBehaviour , IPoolableObject<UndeadScript>
{
    public int _PV;
    public float _Speed;
    public int _Attack;
    public float _AttackSpeed;
    public Transform _Rallypoint;
    public Pool<UndeadScript> Pool { get; set; }
  
    
    
}
