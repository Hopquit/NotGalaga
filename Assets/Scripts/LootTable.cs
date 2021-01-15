using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    public Transform [] drops;
    
    public Transform Loot(float chance)
    {
        if (Random.value >= chance)
        {
            var index = Random.Range(0, drops.Length);
            return drops[index];
        }
        else
        {
            return null;
        }
    }
    void OnDestroy()
    {
        var drop = Loot(0.2f);
        if (drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }
}
