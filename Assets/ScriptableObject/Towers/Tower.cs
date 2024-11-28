using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Tower", menuName = "Scriptable Objects/Towers",order = 1)]
public class Tower : ScriptableObject
{
    [SerializeField] private GameObject towerPrefab;
    [FormerlySerializedAs("_spawnRate")] [Min(0.0f)] public float _Cooldown;
    [Min(0.0f)] public float _range;
    public int _price;
    public bool _IsTrap;
    public  GameObject  _Tower{get { return towerPrefab; } } 
}
