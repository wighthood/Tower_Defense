using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindTarget : MonoBehaviour
{
    private List<GameObject> targets = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        targets.Add(other.gameObject);
        targets.OrderBy(x=>other.gameObject.GetComponent<EnemyScript>()._Distance).ToList();
    }

    public GameObject GetTarget()
    {
        if (targets.Count >= 0)
        {
            return targets.FirstOrDefault();
        }
        return null;
    }
        
    
    private void OnTriggerExit(Collider other)
    {
        targets.Remove(other.gameObject);
        targets.OrderBy(x=>other.gameObject.GetComponent<EnemyScript>()._Distance).ToList();
    }
}

