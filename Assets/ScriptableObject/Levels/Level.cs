using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    public int level;
    public float spawnrate;
    public List<EnemyWithQuantity> enemies;
}
