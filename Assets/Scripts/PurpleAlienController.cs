using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleAlienController : MonoBehaviour

{
    public Transform bullet;
    public float secondsPerBullet = 0.25f;
    float cooldown = 0;
    float firingAngle = 0;
    public float firingAngleSpeed = 40;
    void Start()
    {
        
    }

  
    void Update()
    {
        Vector3 direction = -transform.position;
        transform.position += direction * Time.deltaTime;
        if (cooldown <= 0)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, firingAngle);
            Instantiate(bullet, transform.position, rotation);
            cooldown = secondsPerBullet;
        }
        cooldown -= Time.deltaTime;
        firingAngle += Time.deltaTime * firingAngleSpeed;
        
    }
}
