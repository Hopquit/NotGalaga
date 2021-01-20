using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "ScriptableObjects/LootTable")]
public class LootTableScriptableObject : ScriptableObject

{
   public Transform [] loot;
   public float chance;
   public Transform RandomDrop()
   {
      if (Random.value <= chance)
        {
            var index = Random.Range(0, loot.Length);
            return loot[index];
        }
        else
        {
            return null;
        }
   }

}
