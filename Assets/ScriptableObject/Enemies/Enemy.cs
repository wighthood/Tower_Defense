using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy",order = 1)]
public class Enemy : ScriptableObject
{
    public int _PV;
    public float _Speed;
    public int _Attack;
}