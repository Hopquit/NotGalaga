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
    }

    
    void Update()
    {
        currentLife -= Time.deltaTime;
        if (currentLife <= 0)
        {
            Destroy (gameObject);
        }
    }
}
