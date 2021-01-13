using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            EventBus.TriggerEvent("AlienDied");
        }
    }
}
