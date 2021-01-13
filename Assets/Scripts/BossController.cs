using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    SpriteRenderer sprite;
    public float speed = 4;
    public Transform bossDestination;
    State state;
    enum State { Spawning, Firing }
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        state = State.Spawning;
    }


    void Update()
    {
        switch (state)
        {
            case State.Spawning:
                Vector3 direction = bossDestination.position - transform.position;
                direction.Normalize();
                transform.position += direction * speed * Time.deltaTime;
                if (Vector3.Distance (transform.position, bossDestination.position)<=0.1)
                {
                    state = State.Firing;
                }
                break;
            case State.Firing:

                break;
        }

    }

}
