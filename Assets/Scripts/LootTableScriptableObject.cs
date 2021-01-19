using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "ScriptableObjects/LootTable")]
public class LootTableScriptableObject : ScriptableObject

{
   public Transform [] loot;
   public float chance;

}
