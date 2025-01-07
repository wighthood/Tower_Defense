using UnityEngine;

[CreateAssetMenu(fileName = "Undead", menuName = "Scriptable Objects/Undead")]
public class Undead : ScriptableObject
{
    public int _PV;
    public float _Speed;
    public int _Attack;
    public float _AttackSpeed;
    public float _LifeTime;
    public float _VisionRange;
    public float _Cooldown;
}
