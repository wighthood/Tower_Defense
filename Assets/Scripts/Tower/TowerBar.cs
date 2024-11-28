using System.Collections.Generic;
using UnityEngine;

public class TowerBar : MonoBehaviour
{
    [SerializeField] private TowerManager _TowerManager;
    [SerializeField] private List<Tower> _Towers = new();

    public void ChangeStructure(int structureIndex) 
    { 
        if (structureIndex < 0 || structureIndex >= _Towers.Count ||_TowerManager._PlacedTower == _Towers[structureIndex])
        {
            _TowerManager._PlacedTower = null;
            return;
        }
        _TowerManager._PlacedTower  = _Towers[structureIndex];
    }
}

