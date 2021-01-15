using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    SpriteRenderer sprite;
    public float speed = 4;
    public Transform bossDestination;
    State state;
    enum State { Spawning, Firing, Lasering }
    public Transform bullet;
    public Transform laser;
    public float secondsPerBullet = 0.75f;
    float cooldown = 0;
    float firingTime = 0;
    public float secondsPerLaser = 7f;
    float laserCooldown = 0;
    public float maxHealth = 10;
    float currentHealth;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        state = State.Spawning;
        laserCooldown = secondsPerLaser;
        currentHealth = maxHealth;
    }


    void Update()
    {
        switch (state)
        {
            case State.Spawning:
                if (bossDestination != null)
                {
                    Vector3 direction = bossDestination.position - transform.position;
                    direction.Normalize();
                    transform.position += direction * speed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, bossDestination.position) <= 0.1)
                    {
                        state = State.Firing;
                    }
                }
                break;
            case State.Firing:
                if (cooldown <= 0)
                {
                    float wiggleAmount = Mathf.Sin(firingTime * 2) * 20;
                    Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0 + wiggleAmount));
                    Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 25 + wiggleAmount));
                    Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -25 + wiggleAmount));
                    cooldown = secondsPerBullet;
                }
                cooldown -= Time.deltaTime;
                firingTime += Time.deltaTime;
                if (laserCooldown <= 0)
                {
                    state = State.Lasering;
                    laserCooldown = secondsPerLaser;
                }
                laserCooldown -= Time.deltaTime;
                break;
            case State.Lasering:
                Instantiate(laser, this.transform, true);
                state = State.Firing;
                break;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            currentHealth -= 1;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
                EventBus.TriggerEvent("BossDied");
            }
        }
    }
    public void SetBossDestination(Transform destination)
    {
        this.bossDestination = destination;
    }

}
