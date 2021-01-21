using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float maxLife = 1;
    float currentLife;
    void Start()
    {
        currentLife = maxLife;
        GetComponent<AudioSource>().volume *= PlayerPrefs.GetFloat("volume");
    }


    void Update()
    {
        currentLife -= Time.deltaTime;
        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
