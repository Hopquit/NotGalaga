using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    public LootTableScriptableObject drops;
    
    public Transform Loot()
    {
        if (Random.value <= drops.chance)
        {
            var index = Random.Range(0, drops.loot.Length);
            return drops.loot[index];
        }
        else
        {
            return null;
        }
    }
    void OnDestroy()
    {
        var drop = Loot();
        if (drop != null)
        {
            drop.position = transform.position;
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }
}
