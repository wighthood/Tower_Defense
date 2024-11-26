using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Scriptable Objects/Towers",order = 1)]
public class Tower : ScriptableObject
{
    [SerializeField] private GameObject towerPrefab;
    [Min(0.0f)] public float _spawnRate;
    [Min(0.0f)] public float _range;
    public int _price;
    public  GameObject  _Tower{get { return towerPrefab; } } 
}
