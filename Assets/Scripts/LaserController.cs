using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float maxDespawnTime = 1;
    float despawnTime;
    void Start()
    {
        despawnTime = maxDespawnTime;
        GetComponent<AudioSource>().volume *= PlayerPrefs.GetFloat("volume");
    }

    
    void Update()
    {
        despawnTime -= Time.deltaTime;
        if (despawnTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
