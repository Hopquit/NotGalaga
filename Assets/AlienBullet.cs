using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour
{
    public float speed = 6;

    public float maxLife = 4;
    float currentLife;


    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }
        currentLife -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(other);
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other);
        if (other.collider.tag == "AlienBullet")
        {
            Destroy(other.gameObject);
        }
    }
}
