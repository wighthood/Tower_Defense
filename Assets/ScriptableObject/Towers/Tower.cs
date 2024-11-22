using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Scriptable Objects/Towers",order = 1)]
public class Tower : ScriptableObject
{
    [Min(0.0f)] public float _spawnRate;
    [Min(0.0f)] public float _range;
    public bool _AOE;
}
